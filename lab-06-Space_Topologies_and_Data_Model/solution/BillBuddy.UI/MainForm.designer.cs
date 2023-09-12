namespace BillBuddy.UI
{
	partial class MainForm
	{
		private System.ComponentModel.IContainer components = null;

		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{              
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		private void InitializeComponent()
		{
            this.OutputGroupBox = new System.Windows.Forms.GroupBox();
            this.ClearOutputButton = new System.Windows.Forms.Button();
            this.OutputTextBox = new System.Windows.Forms.RichTextBox();
            this.OperationsGroupBox = new System.Windows.Forms.GroupBox();
            this.MerchantsFeederButton = new System.Windows.Forms.Button();
            this.UsersFeederButton = new System.Windows.Forms.Button();
            this.OutputGroupBox.SuspendLayout();
            this.OperationsGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // OutputGroupBox
            // 
            this.OutputGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.OutputGroupBox.Controls.Add(this.ClearOutputButton);
            this.OutputGroupBox.Controls.Add(this.OutputTextBox);
            this.OutputGroupBox.Location = new System.Drawing.Point(180, 12);
            this.OutputGroupBox.Name = "OutputGroupBox";
            this.OutputGroupBox.Size = new System.Drawing.Size(739, 550);
            this.OutputGroupBox.TabIndex = 2;
            this.OutputGroupBox.TabStop = false;
            this.OutputGroupBox.Text = "Output";
            // 
            // ClearOutputButton
            // 
            this.ClearOutputButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ClearOutputButton.Location = new System.Drawing.Point(658, 521);
            this.ClearOutputButton.Name = "ClearOutputButton";
            this.ClearOutputButton.Size = new System.Drawing.Size(75, 23);
            this.ClearOutputButton.TabIndex = 1;
            this.ClearOutputButton.Text = "Clear";
            this.ClearOutputButton.UseVisualStyleBackColor = true;
            this.ClearOutputButton.Click += new System.EventHandler(this.ClearOutputButton_Click);
            // 
            // OutputTextBox
            // 
            this.OutputTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.OutputTextBox.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OutputTextBox.ForeColor = System.Drawing.Color.Navy;
            this.OutputTextBox.Location = new System.Drawing.Point(6, 16);
            this.OutputTextBox.Name = "OutputTextBox";
            this.OutputTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth;
            this.OutputTextBox.Size = new System.Drawing.Size(727, 499);
            this.OutputTextBox.TabIndex = 0;
            this.OutputTextBox.Text = "";
            this.OutputTextBox.WordWrap = false;
            // 
            // OperationsGroupBox
            // 
            this.OperationsGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.OperationsGroupBox.Controls.Add(this.MerchantsFeederButton);
            this.OperationsGroupBox.Controls.Add(this.UsersFeederButton);
            this.OperationsGroupBox.Location = new System.Drawing.Point(12, 12);
            this.OperationsGroupBox.Name = "OperationsGroupBox";
            this.OperationsGroupBox.Size = new System.Drawing.Size(162, 550);
            this.OperationsGroupBox.TabIndex = 3;
            this.OperationsGroupBox.TabStop = false;
            this.OperationsGroupBox.Text = "Demos";
            // 
            // MerchantsFeederButton
            // 
            this.MerchantsFeederButton.Location = new System.Drawing.Point(6, 48);
            this.MerchantsFeederButton.Name = "MerchantsFeederButton";
            this.MerchantsFeederButton.Size = new System.Drawing.Size(150, 23);
            this.MerchantsFeederButton.TabIndex = 7;
            this.MerchantsFeederButton.Text = "Merchants Feeder";
            this.MerchantsFeederButton.UseVisualStyleBackColor = true;
            this.MerchantsFeederButton.Click += new System.EventHandler(this.MerchantsFeederButton_Click);
            // 
            // UsersFeederButton
            // 
            this.UsersFeederButton.Location = new System.Drawing.Point(6, 19);
            this.UsersFeederButton.Name = "UsersFeederButton";
            this.UsersFeederButton.Size = new System.Drawing.Size(150, 23);
            this.UsersFeederButton.TabIndex = 6;
            this.UsersFeederButton.Text = "Users Feeder";
            this.UsersFeederButton.UseVisualStyleBackColor = true;
            this.UsersFeederButton.Click += new System.EventHandler(this.UsersFeederButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(931, 574);
            this.Controls.Add(this.OperationsGroupBox);
            this.Controls.Add(this.OutputGroupBox);
            this.Name = "MainForm";
            this.Text = "123Completed Labs - GigaSpaces XAP.NET";
            this.OutputGroupBox.ResumeLayout(false);
            this.OperationsGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

        private System.Windows.Forms.GroupBox OutputGroupBox;
		private System.Windows.Forms.RichTextBox OutputTextBox;
        private System.Windows.Forms.GroupBox OperationsGroupBox;
        private System.Windows.Forms.Button ClearOutputButton;
        private System.Windows.Forms.Button MerchantsFeederButton;
        private System.Windows.Forms.Button UsersFeederButton;
	}
}