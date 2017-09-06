namespace TelegramBot.Host.WindowsService
{
    partial class ProjectInstaller
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.HiDoctor = new System.ServiceProcess.ServiceProcessInstaller();
            this.HiDoctorInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // HiDoctor
            // 
            this.HiDoctor.Account = System.ServiceProcess.ServiceAccount.LocalService;
            this.HiDoctor.Password = null;
            this.HiDoctor.Username = null;
            // 
            // HiDoctorInstaller
            // 
            this.HiDoctorInstaller.DisplayName = "Hi Doctor Service";
            this.HiDoctorInstaller.ServiceName = "Hi Doctor Service";
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.HiDoctor,
            this.HiDoctorInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller HiDoctor;
        private System.ServiceProcess.ServiceInstaller HiDoctorInstaller;
    }
}