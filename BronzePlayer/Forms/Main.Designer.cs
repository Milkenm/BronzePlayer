namespace BronzePlayer
{
	partial class Main
	{
		/// <summary>
		/// Variável de designer necessária.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Limpar os recursos que estão sendo usados.
		/// </summary>
		/// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Código gerado pelo Windows Form Designer

		/// <summary>
		/// Método necessário para suporte ao Designer - não modifique 
		/// o conteúdo deste método com o editor de código.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
			this.filedialog = new System.Windows.Forms.OpenFileDialog();
			this.trackbar_tempomusica = new System.Windows.Forms.TrackBar();
			this.panel_buttons = new System.Windows.Forms.Panel();
			this.button_play = new System.Windows.Forms.Button();
			this.button_pause = new System.Windows.Forms.Button();
			this.button_stop = new System.Windows.Forms.Button();
			this.button_previoustrack = new System.Windows.Forms.Button();
			this.button_backward = new System.Windows.Forms.Button();
			this.button_nexttrack = new System.Windows.Forms.Button();
			this.button_forward = new System.Windows.Forms.Button();
			this.textbox_ytlink = new System.Windows.Forms.TextBox();
			this.tooltip = new System.Windows.Forms.ToolTip(this.components);
			this.timer = new System.Windows.Forms.Timer(this.components);
			this.contextmenustrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.trackbar_volume = new System.Windows.Forms.TrackBar();
			this.checkbox_loop = new System.Windows.Forms.CheckBox();
			this.listbox_playlist = new System.Windows.Forms.ListBox();
			this.timer_loopcheck = new System.Windows.Forms.Timer(this.components);
			this.timer_playnext = new System.Windows.Forms.Timer(this.components);
			this.button_ytexpand = new System.Windows.Forms.Button();
			this.groupbox_ytdownloader = new System.Windows.Forms.GroupBox();
			this.progressbar_ytprogress = new System.Windows.Forms.ProgressBar();
			this.combobox_ytformat = new System.Windows.Forms.ComboBox();
			this.button_ytdownload = new System.Windows.Forms.Button();
			this.folderdialog = new System.Windows.Forms.FolderBrowserDialog();
			this.menu = new System.Windows.Forms.MenuStrip();
			this.menu_file = new System.Windows.Forms.ToolStripMenuItem();
			this.menu_file_open = new System.Windows.Forms.ToolStripMenuItem();
			this.menu_file_separator1 = new System.Windows.Forms.ToolStripSeparator();
			this.menu_file_exit = new System.Windows.Forms.ToolStripMenuItem();
			this.menu_favorites = new System.Windows.Forms.ToolStripMenuItem();
			this.menu_favorites_add = new System.Windows.Forms.ToolStripMenuItem();
			this.menu_favorites_separator1 = new System.Windows.Forms.ToolStripSeparator();
			this.menu_other = new System.Windows.Forms.ToolStripMenuItem();
			this.menu_other_options = new System.Windows.Forms.ToolStripMenuItem();
			this.timer_listentime = new System.Windows.Forms.Timer(this.components);
			this.panel_ytDownloading = new System.Windows.Forms.Panel();
			this.progressBar_ytDownloading = new System.Windows.Forms.ProgressBar();
			this.label_ytDownloading = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.trackbar_tempomusica)).BeginInit();
			this.panel_buttons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.trackbar_volume)).BeginInit();
			this.groupbox_ytdownloader.SuspendLayout();
			this.menu.SuspendLayout();
			this.panel_ytDownloading.SuspendLayout();
			this.SuspendLayout();
			// 
			// filedialog
			// 
			this.filedialog.Filter = "Ficheiro MP3 | *.mp3";
			this.filedialog.FileOk += new System.ComponentModel.CancelEventHandler(this.filedialog_FileOk);
			// 
			// trackbar_tempomusica
			// 
			this.trackbar_tempomusica.BackColor = System.Drawing.Color.Lavender;
			this.trackbar_tempomusica.Location = new System.Drawing.Point(-4, 185);
			this.trackbar_tempomusica.Maximum = 0;
			this.trackbar_tempomusica.Name = "trackbar_tempomusica";
			this.trackbar_tempomusica.Size = new System.Drawing.Size(470, 45);
			this.trackbar_tempomusica.TabIndex = 0;
			this.trackbar_tempomusica.TickStyle = System.Windows.Forms.TickStyle.None;
			this.trackbar_tempomusica.Scroll += new System.EventHandler(this.trackBar_tempoMusica_Scroll);
			// 
			// panel_buttons
			// 
			this.panel_buttons.BackColor = System.Drawing.Color.Lavender;
			this.panel_buttons.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel_buttons.Controls.Add(this.button_play);
			this.panel_buttons.Controls.Add(this.button_pause);
			this.panel_buttons.Controls.Add(this.button_stop);
			this.panel_buttons.Controls.Add(this.button_previoustrack);
			this.panel_buttons.Controls.Add(this.button_backward);
			this.panel_buttons.Controls.Add(this.button_nexttrack);
			this.panel_buttons.Controls.Add(this.button_forward);
			this.panel_buttons.Location = new System.Drawing.Point(1, 210);
			this.panel_buttons.Name = "panel_buttons";
			this.panel_buttons.Size = new System.Drawing.Size(226, 34);
			this.panel_buttons.TabIndex = 7;
			// 
			// button_play
			// 
			this.button_play.BackColor = System.Drawing.Color.White;
			this.button_play.BackgroundImage = global::BronzePlayer.Tralha.Button_Play2;
			this.button_play.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.button_play.Location = new System.Drawing.Point(0, 0);
			this.button_play.Name = "button_play";
			this.button_play.Size = new System.Drawing.Size(32, 32);
			this.button_play.TabIndex = 0;
			this.button_play.UseVisualStyleBackColor = false;
			this.button_play.Click += new System.EventHandler(this.button_play_Click);
			// 
			// button_pause
			// 
			this.button_pause.BackColor = System.Drawing.Color.White;
			this.button_pause.BackgroundImage = global::BronzePlayer.Tralha.Button_Pause2;
			this.button_pause.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.button_pause.Location = new System.Drawing.Point(31, 0);
			this.button_pause.Name = "button_pause";
			this.button_pause.Size = new System.Drawing.Size(32, 32);
			this.button_pause.TabIndex = 0;
			this.button_pause.UseVisualStyleBackColor = false;
			this.button_pause.Click += new System.EventHandler(this.button_pause_Click);
			// 
			// button_stop
			// 
			this.button_stop.BackColor = System.Drawing.Color.White;
			this.button_stop.BackgroundImage = global::BronzePlayer.Tralha.Button_Stop2;
			this.button_stop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.button_stop.Location = new System.Drawing.Point(62, 0);
			this.button_stop.Name = "button_stop";
			this.button_stop.Size = new System.Drawing.Size(32, 32);
			this.button_stop.TabIndex = 0;
			this.button_stop.UseVisualStyleBackColor = false;
			this.button_stop.Click += new System.EventHandler(this.button_stop_Click);
			// 
			// button_previoustrack
			// 
			this.button_previoustrack.BackColor = System.Drawing.Color.White;
			this.button_previoustrack.BackgroundImage = global::BronzePlayer.Tralha.Button_PreviousTrack2;
			this.button_previoustrack.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.button_previoustrack.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.button_previoustrack.Location = new System.Drawing.Point(99, 0);
			this.button_previoustrack.Name = "button_previoustrack";
			this.button_previoustrack.Size = new System.Drawing.Size(32, 32);
			this.button_previoustrack.TabIndex = 0;
			this.button_previoustrack.UseVisualStyleBackColor = false;
			this.button_previoustrack.Click += new System.EventHandler(this.button_previoustrack_Click);
			// 
			// button_backward
			// 
			this.button_backward.BackColor = System.Drawing.Color.White;
			this.button_backward.BackgroundImage = global::BronzePlayer.Tralha.Button_GoBack2;
			this.button_backward.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.button_backward.Location = new System.Drawing.Point(130, 0);
			this.button_backward.Name = "button_backward";
			this.button_backward.Size = new System.Drawing.Size(32, 32);
			this.button_backward.TabIndex = 0;
			this.button_backward.UseVisualStyleBackColor = false;
			this.button_backward.Click += new System.EventHandler(this.button_backward_Click);
			// 
			// button_nexttrack
			// 
			this.button_nexttrack.BackColor = System.Drawing.Color.White;
			this.button_nexttrack.BackgroundImage = global::BronzePlayer.Tralha.Button_NextTrack2;
			this.button_nexttrack.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.button_nexttrack.Location = new System.Drawing.Point(192, 0);
			this.button_nexttrack.Name = "button_nexttrack";
			this.button_nexttrack.Size = new System.Drawing.Size(32, 32);
			this.button_nexttrack.TabIndex = 0;
			this.button_nexttrack.UseVisualStyleBackColor = false;
			this.button_nexttrack.Click += new System.EventHandler(this.button_nexttrack_Click);
			// 
			// button_forward
			// 
			this.button_forward.BackColor = System.Drawing.Color.White;
			this.button_forward.BackgroundImage = global::BronzePlayer.Tralha.Button_GoForward2;
			this.button_forward.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.button_forward.Location = new System.Drawing.Point(161, 0);
			this.button_forward.Name = "button_forward";
			this.button_forward.Size = new System.Drawing.Size(32, 32);
			this.button_forward.TabIndex = 0;
			this.button_forward.UseVisualStyleBackColor = false;
			this.button_forward.Click += new System.EventHandler(this.button_forward_Click);
			// 
			// textbox_ytlink
			// 
			this.textbox_ytlink.BackColor = System.Drawing.Color.White;
			this.textbox_ytlink.Location = new System.Drawing.Point(2, 8);
			this.textbox_ytlink.Name = "textbox_ytlink";
			this.textbox_ytlink.Size = new System.Drawing.Size(336, 20);
			this.textbox_ytlink.TabIndex = 0;
			// 
			// timer
			// 
			this.timer.Interval = 1000;
			this.timer.Tick += new System.EventHandler(this.timer_Tick);
			// 
			// contextmenustrip
			// 
			this.contextmenustrip.Name = "contextMenuStrip1";
			this.contextmenustrip.Size = new System.Drawing.Size(61, 4);
			this.contextmenustrip.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.contextMenuStrip_ItemClicked);
			// 
			// trackbar_volume
			// 
			this.trackbar_volume.BackColor = System.Drawing.Color.Lavender;
			this.trackbar_volume.LargeChange = 10;
			this.trackbar_volume.Location = new System.Drawing.Point(312, 212);
			this.trackbar_volume.Maximum = 100;
			this.trackbar_volume.Name = "trackbar_volume";
			this.trackbar_volume.Size = new System.Drawing.Size(127, 45);
			this.trackbar_volume.TabIndex = 0;
			this.trackbar_volume.Scroll += new System.EventHandler(this.trackBar_volume_Scroll);
			// 
			// checkbox_loop
			// 
			this.checkbox_loop.AutoSize = true;
			this.checkbox_loop.BackColor = System.Drawing.Color.Lavender;
			this.checkbox_loop.Location = new System.Drawing.Point(228, 209);
			this.checkbox_loop.Name = "checkbox_loop";
			this.checkbox_loop.Size = new System.Drawing.Size(50, 17);
			this.checkbox_loop.TabIndex = 8;
			this.checkbox_loop.Text = "Loop";
			this.checkbox_loop.UseVisualStyleBackColor = false;
			this.checkbox_loop.CheckedChanged += new System.EventHandler(this.checkbox_loop_CheckedChanged);
			// 
			// listbox_playlist
			// 
			this.listbox_playlist.AllowDrop = true;
			this.listbox_playlist.BackColor = System.Drawing.Color.White;
			this.listbox_playlist.FormattingEnabled = true;
			this.listbox_playlist.Location = new System.Drawing.Point(-1, 24);
			this.listbox_playlist.Name = "listbox_playlist";
			this.listbox_playlist.Size = new System.Drawing.Size(464, 160);
			this.listbox_playlist.TabIndex = 0;
			this.listbox_playlist.Click += new System.EventHandler(this.listbox_playlist_Click);
			this.listbox_playlist.DragDrop += new System.Windows.Forms.DragEventHandler(this.listbox_playlist_DragDrop);
			this.listbox_playlist.DragEnter += new System.Windows.Forms.DragEventHandler(this.listbox_playlist_DragEnter);
			this.listbox_playlist.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listBox_playlist_MouseDown);
			// 
			// timer_loopcheck
			// 
			this.timer_loopcheck.Interval = 1;
			this.timer_loopcheck.Tick += new System.EventHandler(this.timer_loopcheck_Tick);
			// 
			// timer_playnext
			// 
			this.timer_playnext.Interval = 1;
			this.timer_playnext.Tick += new System.EventHandler(this.timer_playnext_Tick);
			// 
			// button_ytexpand
			// 
			this.button_ytexpand.Location = new System.Drawing.Point(440, 223);
			this.button_ytexpand.Name = "button_ytexpand";
			this.button_ytexpand.Size = new System.Drawing.Size(22, 22);
			this.button_ytexpand.TabIndex = 9;
			this.button_ytexpand.Text = "▼";
			this.button_ytexpand.UseVisualStyleBackColor = true;
			this.button_ytexpand.Click += new System.EventHandler(this.button_ytexpand_Click);
			// 
			// groupbox_ytdownloader
			// 
			this.groupbox_ytdownloader.Controls.Add(this.progressbar_ytprogress);
			this.groupbox_ytdownloader.Controls.Add(this.combobox_ytformat);
			this.groupbox_ytdownloader.Controls.Add(this.button_ytdownload);
			this.groupbox_ytdownloader.Controls.Add(this.textbox_ytlink);
			this.groupbox_ytdownloader.Location = new System.Drawing.Point(1, 245);
			this.groupbox_ytdownloader.Name = "groupbox_ytdownloader";
			this.groupbox_ytdownloader.Size = new System.Drawing.Size(460, 44);
			this.groupbox_ytdownloader.TabIndex = 10;
			this.groupbox_ytdownloader.TabStop = false;
			// 
			// progressbar_ytprogress
			// 
			this.progressbar_ytprogress.Location = new System.Drawing.Point(2, 29);
			this.progressbar_ytprogress.Name = "progressbar_ytprogress";
			this.progressbar_ytprogress.Size = new System.Drawing.Size(456, 13);
			this.progressbar_ytprogress.TabIndex = 13;
			// 
			// combobox_ytformat
			// 
			this.combobox_ytformat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.combobox_ytformat.FormattingEnabled = true;
			this.combobox_ytformat.Items.AddRange(new object[] {
            ".mp3",
            ".mp4"});
			this.combobox_ytformat.Location = new System.Drawing.Point(339, 8);
			this.combobox_ytformat.Name = "combobox_ytformat";
			this.combobox_ytformat.Size = new System.Drawing.Size(47, 21);
			this.combobox_ytformat.TabIndex = 0;
			// 
			// button_ytdownload
			// 
			this.button_ytdownload.Location = new System.Drawing.Point(386, 7);
			this.button_ytdownload.Name = "button_ytdownload";
			this.button_ytdownload.Size = new System.Drawing.Size(73, 22);
			this.button_ytdownload.TabIndex = 0;
			this.button_ytdownload.Text = "Download";
			this.button_ytdownload.UseVisualStyleBackColor = true;
			this.button_ytdownload.Click += new System.EventHandler(this.button_ytdownload_Click);
			// 
			// menu
			// 
			this.menu.BackgroundImage = global::BronzePlayer.Tralha.Background_Padron;
			this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_file,
            this.menu_favorites,
            this.menu_other});
			this.menu.Location = new System.Drawing.Point(0, 0);
			this.menu.Name = "menu";
			this.menu.Size = new System.Drawing.Size(462, 24);
			this.menu.TabIndex = 11;
			this.menu.Text = "menuStrip1";
			// 
			// menu_file
			// 
			this.menu_file.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this.menu_file.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_file_open,
            this.menu_file_separator1,
            this.menu_file_exit});
			this.menu_file.Font = new System.Drawing.Font("Segoe UI Black", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.menu_file.ForeColor = System.Drawing.Color.White;
			this.menu_file.Name = "menu_file";
			this.menu_file.Size = new System.Drawing.Size(40, 20);
			this.menu_file.Text = "File";
			// 
			// menu_file_open
			// 
			this.menu_file_open.Font = new System.Drawing.Font("Segoe UI Black", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.menu_file_open.Name = "menu_file_open";
			this.menu_file_open.ShortcutKeyDisplayString = "Ctrl + O";
			this.menu_file_open.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
			this.menu_file_open.Size = new System.Drawing.Size(171, 22);
			this.menu_file_open.Text = "Open...";
			this.menu_file_open.Click += new System.EventHandler(this.menu_file_open_Click);
			// 
			// menu_file_separator1
			// 
			this.menu_file_separator1.Name = "menu_file_separator1";
			this.menu_file_separator1.Size = new System.Drawing.Size(168, 6);
			// 
			// menu_file_exit
			// 
			this.menu_file_exit.Name = "menu_file_exit";
			this.menu_file_exit.ShortcutKeyDisplayString = "Alt + F4";
			this.menu_file_exit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
			this.menu_file_exit.Size = new System.Drawing.Size(171, 22);
			this.menu_file_exit.Text = "Exit";
			this.menu_file_exit.Click += new System.EventHandler(this.menu_file_exit_Click);
			// 
			// menu_favorites
			// 
			this.menu_favorites.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_favorites_add,
            this.menu_favorites_separator1});
			this.menu_favorites.Font = new System.Drawing.Font("Segoe UI Black", 9F, System.Drawing.FontStyle.Bold);
			this.menu_favorites.ForeColor = System.Drawing.Color.White;
			this.menu_favorites.Name = "menu_favorites";
			this.menu_favorites.Size = new System.Drawing.Size(74, 20);
			this.menu_favorites.Text = "Favorites";
			this.menu_favorites.Click += new System.EventHandler(this.menu_playlists_Click);
			// 
			// menu_favorites_add
			// 
			this.menu_favorites_add.Name = "menu_favorites_add";
			this.menu_favorites_add.Size = new System.Drawing.Size(99, 22);
			this.menu_favorites_add.Text = "Add";
			this.menu_favorites_add.Click += new System.EventHandler(this.menu_favorites_add_Click);
			// 
			// menu_favorites_separator1
			// 
			this.menu_favorites_separator1.Name = "menu_favorites_separator1";
			this.menu_favorites_separator1.Size = new System.Drawing.Size(96, 6);
			// 
			// menu_other
			// 
			this.menu_other.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_other_options});
			this.menu_other.Font = new System.Drawing.Font("Segoe UI Black", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.menu_other.ForeColor = System.Drawing.Color.White;
			this.menu_other.Name = "menu_other";
			this.menu_other.Size = new System.Drawing.Size(52, 20);
			this.menu_other.Text = "Other";
			// 
			// menu_other_options
			// 
			this.menu_other_options.Name = "menu_other_options";
			this.menu_other_options.Size = new System.Drawing.Size(122, 22);
			this.menu_other_options.Text = "Options";
			this.menu_other_options.Click += new System.EventHandler(this.menu_other_options_Click);
			// 
			// timer_listentime
			// 
			this.timer_listentime.Interval = 1000;
			this.timer_listentime.Tick += new System.EventHandler(this.timer_listentime_Tick);
			// 
			// panel_ytDownloading
			// 
			this.panel_ytDownloading.Controls.Add(this.progressBar_ytDownloading);
			this.panel_ytDownloading.Controls.Add(this.label_ytDownloading);
			this.panel_ytDownloading.Location = new System.Drawing.Point(131, 53);
			this.panel_ytDownloading.Name = "panel_ytDownloading";
			this.panel_ytDownloading.Size = new System.Drawing.Size(200, 100);
			this.panel_ytDownloading.TabIndex = 12;
			this.panel_ytDownloading.Visible = false;
			// 
			// progressBar_ytDownloading
			// 
			this.progressBar_ytDownloading.Location = new System.Drawing.Point(15, 47);
			this.progressBar_ytDownloading.Name = "progressBar_ytDownloading";
			this.progressBar_ytDownloading.Size = new System.Drawing.Size(170, 23);
			this.progressBar_ytDownloading.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
			this.progressBar_ytDownloading.TabIndex = 1;
			// 
			// label_ytDownloading
			// 
			this.label_ytDownloading.AutoSize = true;
			this.label_ytDownloading.Location = new System.Drawing.Point(61, 30);
			this.label_ytDownloading.Name = "label_ytDownloading";
			this.label_ytDownloading.Size = new System.Drawing.Size(78, 13);
			this.label_ytDownloading.TabIndex = 0;
			this.label_ytDownloading.Text = "Downloading...";
			// 
			// Main
			// 
			this.AllowDrop = true;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Lavender;
			this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.ClientSize = new System.Drawing.Size(462, 290);
			this.Controls.Add(this.panel_ytDownloading);
			this.Controls.Add(this.menu);
			this.Controls.Add(this.groupbox_ytdownloader);
			this.Controls.Add(this.button_ytexpand);
			this.Controls.Add(this.checkbox_loop);
			this.Controls.Add(this.trackbar_volume);
			this.Controls.Add(this.panel_buttons);
			this.Controls.Add(this.trackbar_tempomusica);
			this.Controls.Add(this.listbox_playlist);
			this.DoubleBuffered = true;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.menu;
			this.MaximizeBox = false;
			this.Name = "Main";
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "Bronze Player";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
			((System.ComponentModel.ISupportInitialize)(this.trackbar_tempomusica)).EndInit();
			this.panel_buttons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.trackbar_volume)).EndInit();
			this.groupbox_ytdownloader.ResumeLayout(false);
			this.groupbox_ytdownloader.PerformLayout();
			this.menu.ResumeLayout(false);
			this.menu.PerformLayout();
			this.panel_ytDownloading.ResumeLayout(false);
			this.panel_ytDownloading.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.OpenFileDialog filedialog;
		private System.Windows.Forms.TrackBar trackbar_tempomusica;
		private System.Windows.Forms.Button button_play;
		private System.Windows.Forms.Button button_pause;
		private System.Windows.Forms.Button button_stop;
		private System.Windows.Forms.Panel panel_buttons;
		private System.Windows.Forms.Button button_previoustrack;
		private System.Windows.Forms.Button button_backward;
		private System.Windows.Forms.Button button_forward;
		private System.Windows.Forms.Button button_nexttrack;
		private System.Windows.Forms.TextBox textbox_ytlink;
        private System.Windows.Forms.ToolTip tooltip;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.ContextMenuStrip contextmenustrip;
        private System.Windows.Forms.TrackBar trackbar_volume;
        private System.Windows.Forms.CheckBox checkbox_loop;
        private System.Windows.Forms.ListBox listbox_playlist;
        private System.Windows.Forms.Timer timer_loopcheck;
        private System.Windows.Forms.Timer timer_playnext;
        private System.Windows.Forms.Button button_ytexpand;
        private System.Windows.Forms.GroupBox groupbox_ytdownloader;
        private System.Windows.Forms.Button button_ytdownload;
        private System.Windows.Forms.ComboBox combobox_ytformat;
        private System.Windows.Forms.FolderBrowserDialog folderdialog;
        private System.Windows.Forms.ProgressBar progressbar_ytprogress;
        private System.Windows.Forms.MenuStrip menu;
        private System.Windows.Forms.ToolStripMenuItem menu_file;
        private System.Windows.Forms.ToolStripMenuItem menu_file_open;
        private System.Windows.Forms.ToolStripSeparator menu_file_separator1;
        private System.Windows.Forms.ToolStripMenuItem menu_file_exit;
        private System.Windows.Forms.ToolStripMenuItem menu_other;
        private System.Windows.Forms.ToolStripMenuItem menu_other_options;
        private System.Windows.Forms.ToolStripMenuItem menu_favorites;
        private System.Windows.Forms.Timer timer_listentime;
        private System.Windows.Forms.Panel panel_ytDownloading;
        private System.Windows.Forms.ProgressBar progressBar_ytDownloading;
        private System.Windows.Forms.Label label_ytDownloading;
        private System.Windows.Forms.ToolStripMenuItem menu_favorites_add;
        private System.Windows.Forms.ToolStripSeparator menu_favorites_separator1;
    }
}

