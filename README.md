# Shopify Store Variant Automation Using SELENIUM IDE

This repository contains a C# application for automating variant manipulation tasks in a Shopify store using Selenium IDE.

## Overview
The application provides functionalities to manipulate variants of products in a Shopify store based on data imported from an Excel sheet. It utilizes Selenium IDE for web automation.

## Features
- Import variant data from an Excel sheet.
- Generate automation code for manipulating variants based on imported data.
- View generated automation code.
- Save automation code to a file in `.side` or `.txt` format.

## Usage
1. **Import Data**: Click on the "Import Data" button to select an Excel sheet containing variant data. Ensure that the Excel sheet has headers "Variant Name" and "Variant Color".
2. **View Imported Data**: The imported data will be displayed in a DataGridView.
3. **Generate Code**: Click on the "View Imported Data" button to generate automation code based on the imported data.
4. **View Generated Code**: The generated code will be displayed in a Scintilla control.
5. **Save Code**: Click on the "Save Code" button to save the generated code to a file. Choose the file format as `.side` or `.txt`.

## Requirements
- .NET Framework
- Selenium IDE
- ClosedXML.Excel
- ScintillaNET

## License
This project is licensed under the [MIT License](LICENSE).
