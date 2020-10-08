namespace SeleniumHowKteam
{
    partial class Form1
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
            this.btnFirefox = new System.Windows.Forms.Button();
            this.btnProcessImage = new System.Windows.Forms.Button();
            this.picImageCaptcha = new System.Windows.Forms.PictureBox();
            this.txtCaptchaCode = new System.Windows.Forms.RichTextBox();
            this.btnUploadImage = new System.Windows.Forms.Button();
            this.btnGetCaptChaCode = new System.Windows.Forms.Button();
            this.btnSearchNguoiNopThue = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picImageCaptcha)).BeginInit();
            this.SuspendLayout();
            // 
            // btnFirefox
            // 
            this.btnFirefox.Location = new System.Drawing.Point(12, 24);
            this.btnFirefox.Name = "btnFirefox";
            this.btnFirefox.Size = new System.Drawing.Size(269, 23);
            this.btnFirefox.TabIndex = 0;
            this.btnFirefox.Text = "Tra cứu thông tin hoá đơn";
            this.btnFirefox.UseVisualStyleBackColor = true;
            this.btnFirefox.Click += new System.EventHandler(this.btnFirefox_Click);
            // 
            // btnProcessImage
            // 
            this.btnProcessImage.Location = new System.Drawing.Point(12, 217);
            this.btnProcessImage.Name = "btnProcessImage";
            this.btnProcessImage.Size = new System.Drawing.Size(123, 23);
            this.btnProcessImage.TabIndex = 0;
            this.btnProcessImage.Text = "Process Image";
            this.btnProcessImage.UseVisualStyleBackColor = true;
            this.btnProcessImage.Click += new System.EventHandler(this.btnProcessImage_Click);
            // 
            // picImageCaptcha
            // 
            this.picImageCaptcha.Location = new System.Drawing.Point(12, 66);
            this.picImageCaptcha.Name = "picImageCaptcha";
            this.picImageCaptcha.Size = new System.Drawing.Size(128, 117);
            this.picImageCaptcha.TabIndex = 1;
            this.picImageCaptcha.TabStop = false;
            // 
            // txtCaptchaCode
            // 
            this.txtCaptchaCode.Location = new System.Drawing.Point(158, 66);
            this.txtCaptchaCode.Name = "txtCaptchaCode";
            this.txtCaptchaCode.Size = new System.Drawing.Size(123, 116);
            this.txtCaptchaCode.TabIndex = 2;
            this.txtCaptchaCode.Text = "";
            // 
            // btnUploadImage
            // 
            this.btnUploadImage.Location = new System.Drawing.Point(12, 188);
            this.btnUploadImage.Name = "btnUploadImage";
            this.btnUploadImage.Size = new System.Drawing.Size(123, 23);
            this.btnUploadImage.TabIndex = 0;
            this.btnUploadImage.Text = "Upload Image";
            this.btnUploadImage.UseVisualStyleBackColor = true;
            this.btnUploadImage.Click += new System.EventHandler(this.btnUploadImage_Click);
            // 
            // btnGetCaptChaCode
            // 
            this.btnGetCaptChaCode.Location = new System.Drawing.Point(158, 188);
            this.btnGetCaptChaCode.Name = "btnGetCaptChaCode";
            this.btnGetCaptChaCode.Size = new System.Drawing.Size(123, 23);
            this.btnGetCaptChaCode.TabIndex = 0;
            this.btnGetCaptChaCode.Text = "Get CaptchaCode";
            this.btnGetCaptChaCode.UseVisualStyleBackColor = true;
            this.btnGetCaptChaCode.Click += new System.EventHandler(this.btnGetCaptChaCode_Click);
            // 
            // btnSearchNguoiNopThue
            // 
            this.btnSearchNguoiNopThue.Location = new System.Drawing.Point(300, 24);
            this.btnSearchNguoiNopThue.Name = "btnSearchNguoiNopThue";
            this.btnSearchNguoiNopThue.Size = new System.Drawing.Size(269, 23);
            this.btnSearchNguoiNopThue.TabIndex = 0;
            this.btnSearchNguoiNopThue.Text = "Tra cứu thông tin người nộp thuế";
            this.btnSearchNguoiNopThue.UseVisualStyleBackColor = true;
            this.btnSearchNguoiNopThue.Click += new System.EventHandler(this.btnSearchNguoiNopThue_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(715, 354);
            this.Controls.Add(this.txtCaptchaCode);
            this.Controls.Add(this.picImageCaptcha);
            this.Controls.Add(this.btnUploadImage);
            this.Controls.Add(this.btnGetCaptChaCode);
            this.Controls.Add(this.btnProcessImage);
            this.Controls.Add(this.btnSearchNguoiNopThue);
            this.Controls.Add(this.btnFirefox);
            this.Name = "Form1";
            this.Text = "Selenium";
            ((System.ComponentModel.ISupportInitialize)(this.picImageCaptcha)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnFirefox;
        private System.Windows.Forms.Button btnProcessImage;
        private System.Windows.Forms.PictureBox picImageCaptcha;
        private System.Windows.Forms.RichTextBox txtCaptchaCode;
        private System.Windows.Forms.Button btnUploadImage;
        private System.Windows.Forms.Button btnGetCaptChaCode;
        private System.Windows.Forms.Button btnSearchNguoiNopThue;
    }
}

