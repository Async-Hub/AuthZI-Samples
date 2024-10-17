using Microsoft.VisualBasic.Logging;

namespace DesktopClient
{
	partial class Main
	{
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
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
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			login = new Button();
			accessTokenLabel = new Label();
			accessTokenTextBox = new TextBox();
			responseTextBox = new TextBox();
			loadTimeButton = new Button();
			SuspendLayout();
			// 
			// login
			// 
			login.Location = new Point(591, 448);
			login.Name = "login";
			login.Size = new Size(75, 38);
			login.TabIndex = 0;
			login.Text = "Login";
			login.UseVisualStyleBackColor = true;
			login.Click += login_Click;
			// 
			// accessTokenLabel
			// 
			accessTokenLabel.AutoSize = true;
			accessTokenLabel.Location = new Point(12, 9);
			accessTokenLabel.Name = "accessTokenLabel";
			accessTokenLabel.Size = new Size(70, 15);
			accessTokenLabel.TabIndex = 1;
			accessTokenLabel.Text = "Logged Out";
			// 
			// accessTokenTextBox
			// 
			accessTokenTextBox.Location = new Point(12, 42);
			accessTokenTextBox.Multiline = true;
			accessTokenTextBox.Name = "accessTokenTextBox";
			accessTokenTextBox.Size = new Size(654, 202);
			accessTokenTextBox.TabIndex = 2;
			// 
			// responseTextBox
			// 
			responseTextBox.Location = new Point(12, 250);
			responseTextBox.Multiline = true;
			responseTextBox.Name = "responseTextBox";
			responseTextBox.Size = new Size(654, 163);
			responseTextBox.TabIndex = 2;
			// 
			// loadTimeButton
			// 
			loadTimeButton.Enabled = false;
			loadTimeButton.Location = new Point(497, 448);
			loadTimeButton.Name = "loadTimeButton";
			loadTimeButton.Size = new Size(88, 38);
			loadTimeButton.TabIndex = 3;
			loadTimeButton.Text = "Load Time";
			loadTimeButton.UseVisualStyleBackColor = true;
			loadTimeButton.Click += loadTimeButton_Click;
			// 
			// Main
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(678, 498);
			Controls.Add(loadTimeButton);
			Controls.Add(responseTextBox);
			Controls.Add(accessTokenTextBox);
			Controls.Add(accessTokenLabel);
			Controls.Add(login);
			FormBorderStyle = FormBorderStyle.FixedDialog;
			Name = "Main";
			Text = "Desktop Client";
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private System.Windows.Forms.Button login;
		private System.Windows.Forms.Label accessTokenLabel;
		private System.Windows.Forms.TextBox accessTokenTextBox;
		private System.Windows.Forms.TextBox responseTextBox;
		private System.Windows.Forms.Button loadTimeButton;
	}
}