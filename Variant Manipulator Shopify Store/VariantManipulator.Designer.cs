namespace Variant_Manipulator_Shopify_Store
{
    partial class VariantManipulator
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VariantManipulator));
            this.urlTargeted = new System.Windows.Forms.TextBox();
            this.importview = new System.Windows.Forms.DataGridView();
            this.labelURL = new System.Windows.Forms.Label();
            this.labelEXCELVIEW = new System.Windows.Forms.Label();
            this.codeEditor = new ScintillaNET.Scintilla();
            this.labelCODEEDITOR = new System.Windows.Forms.Label();
            this.saveimportdata = new System.Windows.Forms.Button();
            this.viewimportdata = new System.Windows.Forms.Button();
            this.importdata = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.importview)).BeginInit();
            this.SuspendLayout();
            // 
            // urlTargeted
            // 
            this.urlTargeted.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.urlTargeted.Font = new System.Drawing.Font("Microsoft YaHei UI Light", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.urlTargeted.Location = new System.Drawing.Point(40, 40);
            this.urlTargeted.Name = "urlTargeted";
            this.urlTargeted.Size = new System.Drawing.Size(1164, 43);
            this.urlTargeted.TabIndex = 0;
            // 
            // importview
            // 
            this.importview.AllowUserToAddRows = false;
            this.importview.BackgroundColor = System.Drawing.SystemColors.Control;
            this.importview.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.importview.Location = new System.Drawing.Point(39, 135);
            this.importview.Name = "importview";
            this.importview.RowHeadersWidth = 51;
            this.importview.RowTemplate.Height = 24;
            this.importview.Size = new System.Drawing.Size(968, 251);
            this.importview.TabIndex = 1;
            // 
            // labelURL
            // 
            this.labelURL.AutoSize = true;
            this.labelURL.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelURL.Location = new System.Drawing.Point(35, 10);
            this.labelURL.Name = "labelURL";
            this.labelURL.Size = new System.Drawing.Size(157, 27);
            this.labelURL.TabIndex = 2;
            this.labelURL.Text = "TARGETED URL";
            // 
            // labelEXCELVIEW
            // 
            this.labelEXCELVIEW.AutoSize = true;
            this.labelEXCELVIEW.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelEXCELVIEW.Location = new System.Drawing.Point(35, 105);
            this.labelEXCELVIEW.Name = "labelEXCELVIEW";
            this.labelEXCELVIEW.Size = new System.Drawing.Size(179, 27);
            this.labelEXCELVIEW.TabIndex = 3;
            this.labelEXCELVIEW.Text = "EXCELVIEW AREA";
            // 
            // codeEditor
            // 
            this.codeEditor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.codeEditor.Location = new System.Drawing.Point(40, 443);
            this.codeEditor.Name = "codeEditor";
            this.codeEditor.Size = new System.Drawing.Size(1164, 380);
            this.codeEditor.TabIndex = 4;
            // 
            // labelCODEEDITOR
            // 
            this.labelCODEEDITOR.AutoSize = true;
            this.labelCODEEDITOR.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCODEEDITOR.Location = new System.Drawing.Point(35, 413);
            this.labelCODEEDITOR.Name = "labelCODEEDITOR";
            this.labelCODEEDITOR.Size = new System.Drawing.Size(145, 27);
            this.labelCODEEDITOR.TabIndex = 5;
            this.labelCODEEDITOR.Text = "CODE EDITOR";
            // 
            // saveimportdata
            // 
            this.saveimportdata.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saveimportdata.Location = new System.Drawing.Point(1022, 316);
            this.saveimportdata.Name = "saveimportdata";
            this.saveimportdata.Size = new System.Drawing.Size(182, 48);
            this.saveimportdata.TabIndex = 7;
            this.saveimportdata.Text = "SAVE DATA";
            this.saveimportdata.UseVisualStyleBackColor = true;
            this.saveimportdata.Click += new System.EventHandler(this.saveimportdata_Click);
            // 
            // viewimportdata
            // 
            this.viewimportdata.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.viewimportdata.Location = new System.Drawing.Point(1022, 236);
            this.viewimportdata.Name = "viewimportdata";
            this.viewimportdata.Size = new System.Drawing.Size(182, 48);
            this.viewimportdata.TabIndex = 8;
            this.viewimportdata.Text = "VIEW DATA";
            this.viewimportdata.UseVisualStyleBackColor = true;
            this.viewimportdata.Click += new System.EventHandler(this.viewimportdata_Click);
            // 
            // importdata
            // 
            this.importdata.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.importdata.Location = new System.Drawing.Point(1022, 157);
            this.importdata.Name = "importdata";
            this.importdata.Size = new System.Drawing.Size(182, 48);
            this.importdata.TabIndex = 9;
            this.importdata.Text = "IMPORT DATA";
            this.importdata.UseVisualStyleBackColor = true;
            this.importdata.Click += new System.EventHandler(this.importdata_Click);
            // 
            // VariantManipulator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1239, 877);
            this.Controls.Add(this.importdata);
            this.Controls.Add(this.viewimportdata);
            this.Controls.Add(this.saveimportdata);
            this.Controls.Add(this.labelCODEEDITOR);
            this.Controls.Add(this.codeEditor);
            this.Controls.Add(this.labelEXCELVIEW);
            this.Controls.Add(this.labelURL);
            this.Controls.Add(this.importview);
            this.Controls.Add(this.urlTargeted);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "VariantManipulator";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "VARIANT MANIPULATOR SHOPIFY STORE";
            ((System.ComponentModel.ISupportInitialize)(this.importview)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox urlTargeted;
        private System.Windows.Forms.DataGridView importview;
        private System.Windows.Forms.Label labelURL;
        private System.Windows.Forms.Label labelEXCELVIEW;
        private ScintillaNET.Scintilla codeEditor;
        private System.Windows.Forms.Label labelCODEEDITOR;
        private System.Windows.Forms.Button saveimportdata;
        private System.Windows.Forms.Button viewimportdata;
        private System.Windows.Forms.Button importdata;
    }
}

