using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClosedXML.Excel;
using ScintillaNET;

namespace Variant_Manipulator_Shopify_Store
{
    public partial class VariantManipulator : Form
    {
        string targetedUrl, finalCode, startPath, startPathID, endPath, endPathID, variantNameID, variantNamePathStart, variantNamePathEnd, metaFieldID, metaFieldPathEnd, colorValueID, colorValuePathEnd, nextUrlPathStart, nextUrlID, nextUrlPathend, saveBtnID, saveBtnPathEnd;
        bool targetUrlEntered = false;
        bool excelFileImported = false;
        bool dataViewed = false;
        private void viewimportdata_Click(object sender, EventArgs e)
        {
            Random rand = new Random();
            //viewarea.Hide();
            //DownloadFinalCode.Show();
            string[] rv = new string[5];
            for (int i = 0; i <= 4; i++)
            {
                rv[i] = rand.Next(100, 999999).ToString();
            }

            // Generate the final code
            finalCode = startPATH(stringGenerator() + rv[0], stringGenerator() + rv[1], stringGenerator() + rv[2]) + viewData() + endPATH(stringGenerator() + rv[3], stringGenerator() + rv[4]);
            //DownloadFinalCode.Text = finalCode;

            // Display the generated code in the Scintilla control
            codeEditor.Text = finalCode;
            dataViewed = true;
        }
        private void SaveDataToFile(string finalCode)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Side Files (*.side)|*.side|Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            saveFileDialog.DefaultExt = ".side"; // Set default extension to .side

            bool fileSaved = false;

            while (!fileSaved)
            {
                DialogResult dialogResult = saveFileDialog.ShowDialog();
                if (dialogResult == DialogResult.OK)
                {
                    string filePath = saveFileDialog.FileName;

                    if (!targetUrlEntered || !excelFileImported || !dataViewed)
                    {
                        MessageBox.Show("Please complete all steps before saving the file.", "Incomplete Steps", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Check if the file already exists
                    if (File.Exists(filePath))
                    {
                        DialogResult result = MessageBox.Show("A file with the same name already exists. Do you want to overwrite it?", "File Exists", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                        if (result == DialogResult.Yes)
                        {
                            // User wants to overwrite existing file
                            fileSaved = true;
                        }
                        else if (result == DialogResult.No)
                        {
                            // User wants to choose another file name
                            continue;
                        }
                        else
                        {
                            // User canceled the operation
                            return;
                        }
                    }
                    else
                    {
                        // File does not exist, proceed with saving
                        fileSaved = true;
                    }

                    // Write the final code to the file
                    using (StreamWriter sw = new StreamWriter(filePath, false))
                    {
                        sw.WriteLine(finalCode);
                    }

                    MessageBox.Show("File saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (dialogResult == DialogResult.Cancel)
                {
                    // User canceled the operation
                    DialogResult result = MessageBox.Show("Are you sure you want to cancel without saving?", "Cancel Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        return; // User confirmed cancel without saving
                    }
                    // Otherwise, continue loop to prompt again for file save
                }
                else
                {
                    // User closed the dialog without saving
                    DialogResult result = MessageBox.Show("Are you sure you want to close without saving?", "Close Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        return; // User confirmed close without saving
                    }
                    // Otherwise, continue loop to prompt again for file save
                }
            }
        }
        private bool IsUrlValid(string url)
        {
            Uri uriResult;
            return Uri.TryCreate(url, UriKind.Absolute, out uriResult)
                   && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }

        // Event handler for button click to start the process
        private bool StartProcessButton(string urltext)
        {
            bool isTrue = false;
            string targetUrl = urlTargeted.Text.Trim();

            if (IsUrlValid(targetUrl))
            {
                targetUrlEntered = true;
                isTrue = true;
            }
            else
            {
                MessageBox.Show("Please enter a valid URL.", "Invalid URL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return isTrue;
        }

        private void saveimportdata_Click(object sender, EventArgs e)
        {
            if(StartProcessButton(urlTargeted.Text))
                SaveDataToFile(finalCode);
        }

        private async void importdata_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel Sheet(*.xlsx)|*.xlsx|All Files(*.*)|*.*";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    DataTable dt = new DataTable();
                    bool headerVerified = false;
                    bool fileOpened = false;
                    const int maxRetries = 3;
                    int retries = 0;

                    while (!fileOpened && retries < maxRetries)
                    {
                        try
                        {
                            using (XLWorkbook workbook = new XLWorkbook(openFileDialog.FileName))
                            {
                                var worksheet = workbook.Worksheet(1);
                                var rows = worksheet.RowsUsed();

                                // Check header names
                                var headerRow = worksheet.Row(1);
                                if (headerRow != null && headerRow.CellCount() >= 2)
                                {
                                    var header1 = headerRow.Cell(1).Value.ToString();
                                    var header2 = headerRow.Cell(2).Value.ToString();
                                    if (header1 == "Variant Name" && header2 == "Variant Color")
                                        headerVerified = true;
                                }

                                if (headerVerified)
                                {
                                    bool isFirstRow = true;
                                    excelFileImported = true;
                                    foreach (var row in rows)
                                    {
                                        if (isFirstRow)
                                        {
                                            foreach (IXLCell cell in row.Cells())
                                                dt.Columns.Add(cell.Value.ToString());
                                            isFirstRow = false;
                                        }
                                        else
                                        {
                                            dt.Rows.Add();
                                            int i = 0;
                                            foreach (IXLCell cell in row.Cells())
                                            {
                                                dt.Rows[dt.Rows.Count - 1][i++] = cell.Value.ToString();
                                            }
                                        }
                                    }
                                }
                            }

                            fileOpened = true; // Set flag indicating that the file was successfully opened
                        }
                        catch (IOException ex)
                        {
                            retries++; // Increment the retry count
                            if (retries < maxRetries)
                            {
                                // File is in use, wait for a short delay before retrying
                                await Task.Delay(1000); // Wait for 1 second before retrying
                            }
                            else
                            {
                                // Max retries reached, display an error message
                                MessageBox.Show("The file is in use by another process. Please close the file and try again.", "Error Meesage", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                        catch (Exception ex)
                        {
                            // Log the exception
                            Console.WriteLine("An error occurred while loading the file: " + ex.ToString());

                            // Display an error message
                            MessageBox.Show("An error occurred while loading the file. Please see the log for details.", "Error Meesage", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    if (!headerVerified)
                    {
                        MessageBox.Show("The uploaded file does not contain the expected headers 'Variant Name' and 'Variant Color'.", "Error Meesage", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Clear previous data in DataGridView
                    importview.DataSource = null;
                    importview.Columns.Clear();

                    // Set new data source
                    importview.DataSource = dt.DefaultView;
                }
                catch (Exception ex)
                {
                    // Log the exception
                    Console.WriteLine("An unexpected error occurred: " + ex.ToString());

                    // Display an error message
                    MessageBox.Show("An unexpected error occurred. Please see the log for details.", "Error Meesage", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private string startPATH(string ID, string ID2, string ID3)
        {
            string id2, id3;
            startPathID = ID;
            id2 = ID2;
            id3 = ID3;
            //startPath = "{\r\n  \"id\": \""+ startPathID +"\",\r\n  \"version\": \"2.0\",\r\n  \"name\": \"new 2\",\r\n  \"url\": \"https://admin.shopify.com\",\r\n  \"tests\": [{\r\n     \"id\": \"" + id2 + "\",\r\n     \"name\": \"NewCode\",\r\n     \"commands\": [{\r\n        \"id\": \""+ id3 +"\",\r\n        \"comment\": \"\",\r\n        \"command\": \"open\",\r\n        \"target\": \""+ targetedurl.Text +"\",\r\n        \"targets\": [],\r\n        \"value\": \"\"\r\n    }\r\n";
            string start_txt = @"{
  ""id"": ""3ea69f88-2770-44df-a63d-ce88d712218f"",
  ""version"": ""2.0"",
  ""name"": ""new 2"",
  ""url"": ""https://admin.shopify.com"",
  ""tests"": [{
    ""id"": ""bfb1eac4-e1fa-42e0-9dbf-36e4af6f52be"",
    ""name"": ""tst"",
    ""commands"": [{
      ""id"": ""cdc23af2-14c8-43bf-b15e-f4eb9975e830"",
      ""comment"": """",
      ""command"": ""open"",
      ""target"": """;
            string endtxt = @""",
      ""targets"": [],
      ""value"": """"
    }, {
      ""id"": ""6ea18531-3855-47b4-87a4-71e3cd503d7c"",
      ""comment"": """",
      ""command"": ""setWindowSize"",
      ""target"": ""852x816"",
      ""targets"": [],
      ""value"": """"
    }";
            startPath = start_txt + urlTargeted.Text + endtxt;
            return startPath;
        }
        private string endPATH(string ID1, string ID2)
        {
            string id1, id2;
            id1 = ID1;
            id2 = ID2;
            endPath = @"]
  }],
  ""suites"": [{
    ""id"": ""8c5d7204-d742-4d2b-b8d9-73159fe7d1ae"",
    ""name"": ""Default Suite"",
    ""persistSession"": false,
    ""parallel"": false,
    ""timeout"": 300,
    ""tests"": [""bfb1eac4-e1fa-42e0-9dbf-36e4af6f52be""]
  }],
  ""urls"": [""https://admin.shopify.com/""],
  ""plugins"": []
}";
            return endPath;
        }
        private string variantNAME(string valueName, string ID)
        {
            variantNameID = ID;
            string start_txt = @""",
      ""comment"": """",
      ""command"": ""type"",
      ""target"": ""xpath=//main[@id='AppFrameMain']/div/div/div[2]/form/div/div[2]/div/div/div/div[2]/div/div/div/div/div[2]/div/div/input"",
      ""targets"": [
        [""id=:rf9:"", ""id""],
        [""name=selectedOptions.0.value"", ""name""],
        [""css=#\\3Arf9\\3A"", ""css:finder""],
        [""xpath=//input[@id=':rf9:']"", ""xpath:attributes""],
        [""xpath=//main[@id='AppFrameMain']/div/div/div[2]/form/div/div[2]/div/div/div/div[2]/div/div/div/div/div[2]/div/div/input"", ""xpath:idRelative""],
        [""xpath=//input"", ""xpath:position""]
      ],
      ""value"": """;
            //variantNamePathEnd = "\",\r\n      \"comment\": \"\",\r\n      \"command\": \"type\",\r\n      \"target\": \"xpath=//main[@id='AppFrameMain']/div/div/div[2]/form/div/div[2]/div/div/div/div[2]/div/div/div/div/div[2]/div/div/input\",\r\n      \"targets\": [\r\n        [\"id=:rf9:\", \"id\"],\r\n        [\"name=selectedOptions.0.value\", \"name\"],\r\n        [\"css=#\\\\3Arf9\\\\3A\", \"css:finder\"],\r\n        [\"xpath=//input[@id=':rf9:']\", \"xpath:attributes\"],\r\n        [\"xpath=//main[@id='AppFrameMain']/div/div/div[2]/form/div/div[2]/div/div/div/div[2]/div/div/div/div/div[2]/div/div/input\", \"xpath:idRelative\"],\r\n        [\"xpath=//input\", \"xpath:position\"]\r\n      ],\r\n      \"value\": \""+valueName+"\"\r\n    }, {\r\n      \"id\": \"";
            string end_txt = @"""
    }, {
      ""id"": """;
            return @", {
      ""id"": """ + variantNameID + start_txt + valueName + end_txt;
        }
        private string metaFIELD(string ID)
        {
            metaFieldID = ID;
            metaFieldPathEnd = @""",
      ""comment"": """",
      ""command"": ""click"",
      ""target"": ""css=.\\_ReadField_1s08g_14"",
      ""targets"": [
        [""css=.\\_ReadField_1s08g_14"", ""css:finder""],
        [""xpath=//button[@id='metafields.color.value']/div/span/div/div"", ""xpath:idRelative""],
        [""xpath=//div/span/div/div"", ""xpath:position""]
      ],
      ""value"": """"
    }, {
      ""id"": """;
            return metaFieldID + metaFieldPathEnd;
        }
        private string colorPICKER(string ID, string colorValue)
        {
            colorValueID = ID;
            colorValuePathEnd = @""",
      ""comment"": """",
      ""command"": ""type"",
      ""target"": ""xpath=//li/div/div/div/div/div[2]/div/div/input"",
      ""targets"": [
        [""id=:r48:"", ""id""],
        [""css=#\\3Ar48\\3A"", ""css:finder""],
        [""xpath=//input[@id=':r48:']"", ""xpath:attributes""],
        [""xpath=//div[@id='PolarisPortalsContainer']/div[9]/div/div/div/div[2]/div/div[2]/div/div/div/div/ul/li/div/div/div/div/div[2]/div/div/input"", ""xpath:idRelative""],
        [""xpath=//li/div/div/div/div/div[2]/div/div/input"", ""xpath:position""]
      ],
      ""value"": """ + colorValue + @"""
    }, {
      ""id"": """;
            return colorValueID + colorValuePathEnd;
        }
        private string saveBTN(string ID)
        {
            Random random = new Random();
            saveBtnID = ID;
            saveBtnPathEnd = @""",
      ""comment"": """",
      ""command"": ""click"",
      ""target"": ""css=.Polaris-LegacyStack__Item > .Polaris-Button--sizeMedium > span"",
      ""targets"": [
        [""css=.Polaris-LegacyStack__Item > .Polaris-Button--sizeMedium > span"", ""css:finder""],
        [""xpath=//main[@id='AppFrameMain']/div/div/div[2]/form/div/div[3]/div/div/div[2]/button/span"", ""xpath:idRelative""],
        [""xpath=//div[3]/div/div/div[2]/button/span"", ""xpath:position""]
      ],
      ""value"": """"
    }, " + @"{
      ""id"": """+ ID + random.Next(0,100)+@""",
      ""comment"": """",
      ""command"": ""click"",
      ""target"": ""css=.Polaris-Icon--toneInherit > .Polaris-Icon__Svg"",
      ""targets"": [
        [""css=.Polaris-Icon--toneInherit > .Polaris-Icon__Svg"", ""css:finder""]
      ],
      ""value"": """"
    }, {
      ""id"": """;
            return saveBtnID + saveBtnPathEnd;
        }
        private string nextURL(string ID)
        {
            //nextUrlPathStart = ",{\r\n    \"id\":\"";
            nextUrlID = ID;
            nextUrlPathend = @""",
      ""comment"": """",
      ""command"": ""click"",
      ""target"": ""id=nextURL"",
      ""targets"": [
        [""css=#nextURL path"", ""css:finder""]
      ],
      ""value"": """"
    }";

            return nextUrlID + nextUrlPathend;
        }
        private string stringGenerator()
        {
            string randomString, randomString1, randomString2, randomString3, randomString4, randomString5;
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            int lengthPart1 = 8, lengthPart2 = 4, lengthPart3 = 4, lengthPart4 = 4, lengthPart5 = 6;
            randomString1 = new string(Enumerable.Repeat(chars, lengthPart1)
                                              .Select(s => s[random.Next(s.Length)]).ToArray());
            randomString2 = new string(Enumerable.Repeat(chars, lengthPart2)
                                              .Select(s => s[random.Next(s.Length)]).ToArray());
            randomString3 = new string(Enumerable.Repeat(chars, lengthPart3)
                                              .Select(s => s[random.Next(s.Length)]).ToArray());
            randomString4 = new string(Enumerable.Repeat(chars, lengthPart4)
                                              .Select(s => s[random.Next(s.Length)]).ToArray());
            randomString5 = new string(Enumerable.Repeat(chars, lengthPart5)
                                              .Select(s => s[random.Next(s.Length)]).ToArray());
            randomString = randomString1 + "-" + randomString2 + "-" + randomString3 + "-" + randomString4 + "-" + randomString5;

            return randomString;
        }
        private string viewData()
        {
            string viewTxt = "";
            Random rand = new Random();
            for (int i = 0; i < importview.Rows.Count; i++)
            {
                string varName = importview.Rows[i].Cells[0].Value.ToString();
                string colorName = importview.Rows[i].Cells[1].Value.ToString();
                viewTxt += variantNAME(varName, stringGenerator() + rand.Next(100, 999999).ToString()) + metaFIELD(stringGenerator() + rand.Next(100, 999999).ToString()) + colorPICKER(stringGenerator() + rand.Next(100, 999999).ToString(), colorName) + saveBTN(stringGenerator() + rand.Next(100, 999999).ToString()) + nextURL(stringGenerator() + rand.Next(100, 999999).ToString());
            }
            return viewTxt;
        }

        public VariantManipulator()
        {
            InitializeComponent();
            // Set up Scintilla control
            codeEditor.Margins[0].Width = 30;
            codeEditor.Styles[Style.Default].Font = "Consolas";
            codeEditor.Lexer = Lexer.Cpp;

            // Set up markers for line highlighting
            codeEditor.Markers[1].Symbol = MarkerSymbol.Background;
            codeEditor.Markers[1].SetBackColor(System.Drawing.Color.LightGray);


            // Subscribe to events for updating line highlighting
            codeEditor.TextChanged += Scintilla1_TextChanged;

            // Initialize Scintilla control
            codeEditor.Lexer = Lexer.Xml;

            // Subscribe to events
            codeEditor.CharAdded += Scintilla1_CharAdded;
            codeEditor.TextChanged += Scintilla1_TextChanged;
            codeEditor.MouseDown += Scintilla1_MouseDown;
            codeEditor.MouseUp += Scintilla1_MouseUp;
            codeEditor.KeyUp += Scintilla1_KeyUp;
        }

        private void Scintilla1_KeyUp(object sender, KeyEventArgs e)
        {
            if (!mouseIsDown)
            {
                HighlightBraces();
            }
        }

        private void Scintilla1_MouseUp(object sender, MouseEventArgs e)
        {
            mouseIsDown = false;
            HighlightBraces();
        }

        private void Scintilla1_MouseDown(object sender, MouseEventArgs e)
        {
            mouseIsDown = true;
        }

        private List<int> bracePositions = new List<int>();
        private int lastBraceIndex = -1;
        private bool mouseIsDown = false;
        private void Scintilla1_CharAdded(object sender, CharAddedEventArgs e)
        {
            // Clear brace highlights when the text changes
            codeEditor.IndicatorClearRange(0, codeEditor.TextLength);

            // Clear previous brace positions
            bracePositions.Clear();

            // Iterate over the text and track the positions of open and close braces
            for (int i = 0; i < codeEditor.TextLength; i++)
            {
                char currentChar = (char)codeEditor.GetCharAt(i);
                if (currentChar == '{' || currentChar == '}')
                {
                    // Add the position of the brace to the list
                    bracePositions.Add(i);
                }
            }

            // Highlight matching pairs of braces
            HighlightBraces();
        }
        private void HighlightBraces()
        {
            // Remove previous brace highlighting
            codeEditor.IndicatorClearRange(0, codeEditor.TextLength);

            // Remove previous brace highlighting
            codeEditor.IndicatorClearRange(0, codeEditor.TextLength);

            // Highlight matching pairs of braces
            int currentBraceIndex = bracePositions.FindIndex(index => codeEditor.SelectionStart <= index && index < codeEditor.SelectionEnd);
            if (currentBraceIndex != -1)
            {
                int currentBracePosition = bracePositions[currentBraceIndex];
                char currentChar = (char)codeEditor.GetCharAt(currentBracePosition);
                if (currentChar == '{')
                {
                    // Find the position of the matching closing brace
                    int matchingCloseBraceIndex = bracePositions.FindIndex(currentBraceIndex + 1, index => codeEditor.GetCharAt(index) == '}');
                    if (matchingCloseBraceIndex != -1)
                    {
                        int matchingCloseBracePosition = bracePositions[matchingCloseBraceIndex];

                        // Highlight the opening and closing braces
                        codeEditor.IndicatorCurrent = 1;
                        codeEditor.IndicatorFillRange(currentBracePosition, 1);
                        codeEditor.IndicatorFillRange(matchingCloseBracePosition, 1);
                    }
                }
            }
        }

        private void Scintilla1_TextChanged(object sender, EventArgs e)
        {
            // Clear tag highlights when the text changes
            codeEditor.IndicatorClearRange(0, codeEditor.TextLength);
            // Update line highlighting on text change
            UpdateLineHighlighting();

            // Clear brace positions when the text changes
            bracePositions.Clear();
            lastBraceIndex = -1;

            // Iterate over the text and track the positions of open and close braces
            for (int i = 0; i < codeEditor.TextLength; i++)
            {
                char currentChar = (char)codeEditor.GetCharAt(i);
                if (currentChar == '{' || currentChar == '}')
                {
                    bracePositions.Add(i);
                }
            }
        }

        private void UpdateLineHighlighting()
        {
            // Remove existing line highlights
            codeEditor.MarkerDeleteAll(1);

            // Highlight lines with open tags or braces
            string[] openTags = new string[] { "<", "(", "[", "{" };
            for (int i = 0; i < codeEditor.Lines.Count; i++)
            {
                string lineText = codeEditor.Lines[i].Text.Trim();
                foreach (string openTag in openTags)
                {
                    if (lineText.EndsWith(openTag))
                    {
                        codeEditor.Lines[i].MarkerAdd(1);
                        break;
                    }
                }
            }
        }
    }
}
