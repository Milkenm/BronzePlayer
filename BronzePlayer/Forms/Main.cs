#region Using
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

using BronzePlayer.Forms; // [Project] Bronze Player

using NAudio.Wave; // [NuGet] NAudio
#endregion Using

namespace BronzePlayer
{
    public partial class Main : Form
    {
        #region Refers
        Config config = new Config();
        Lang lang = new Lang();
        #endregion Refers

        #region Vars
        // # ================================================================================================================================= #
        bool looping = false, doLoop = false, ytExpanded = false, ignoreSkip = false;
        object listBox_item;
        int listBox_index;
        // # ================================================================================================================================= #
        #endregion

        #region Events
        // # ================================================================================================================================= #
        private void WaveOut_PlaybackStopped(object sender, StoppedEventArgs e)
        {
            try
            {
                if (Scripts.music.state == Scripts.Music.State.Playing)
                {
                    timer.Stop();
                    if (ignoreSkip != true)
                    {
                        #region Loop
                        if (looping == true)
                        {
                            this.Invoke(new Action(() =>
                            {
                                timer_loopcheck.Start();
                            }));
                            doLoop = true;
                        }
                        #endregion Loop
                        else
                        {
                            int maxindex = listbox_playlist.Items.Count - 1;
                            int currentindex = listbox_playlist.SelectedIndex;

                            if (currentindex < maxindex)
                            {
                                this.Invoke(new Action(() =>
                                {
                                    timer_playnext.Start();
                                }));
                            }
                            else
                            {
                                this.Invoke(new Action(() =>
                                {
                                    ToggleButtons(true, false, false, null, null, null, null);
                                }));
                            }
                        }
                    }
                    else
                    {
                        ignoreSkip = false;
                    }
                }
            }
            #region DE3UG
            catch (Exception exception)
            {
                Ex(exception);
            }
            #endregion
        }
        // # ================================================================================================================================= #
        #endregion

        #region Functions
        // # ================================================================================================================================= #
        void Play(string _file)
        {
            try
            {
                trackbar_tempomusica.Enabled = true;
                ToggleButtons(false, true, true, null, null, true, true);
                
                Scripts.music.Play(_file);

                this.Text = "Bronze Player - " + Path.GetFileNameWithoutExtension(Scripts.music.audioFile.FileName);
                
                trackbar_tempomusica.Maximum = Convert.ToInt32(Scripts.music.audioFile.Length);
                trackbar_tempomusica.Value = Convert.ToInt32(Scripts.music.audioFile.Position);
                timer.Start();
            }
            #region DE3UG
            catch (Exception exception)
            {
                Ex(exception);
            }
            #endregion
        }



        void Pause()
        {
            try
            {
                trackbar_tempomusica.Enabled = true;
                ToggleButtons(true, false, true, null, null, false, false);

                ignoreSkip = true;

                Scripts.music.Pause();

                timer.Stop();
            }
            #region DE3UG
            catch (Exception exception)
            {
                Ex(exception);
            }
            #endregion
        }



        void Stop()
        {
            try
            {
                ToggleButtons(true, false, false, null, null, false, false);

                trackbar_tempomusica.Maximum = 0;
                trackbar_tempomusica.Value = 0;
                trackbar_tempomusica.Enabled = false;

                ignoreSkip = true;

                Scripts.music.Stop();

                this.Text = "Bronze Player";

                timer.Stop();
            }
            #region DE3UG
            catch (Exception exception)
            {
                Ex(exception);
            }
            #endregion
        }



        void PlayNext()
        {
            try
            {
                ignoreSkip = true;

                int maxindex = listbox_playlist.Items.Count - 1;
                int currentindex = listbox_playlist.SelectedIndex;

                if (currentindex < maxindex)
                {
                    if (Scripts.music.state == Scripts.Music.State.Playing)
                    {
                        ToggleButtons(false, false, true, null, null, null, null);
                    }
                    if (currentindex + 1 == maxindex)
                    {
                        ToggleButtons(null, null, null, null, false, null, null);
                    }
                    ToggleButtons(null, null, null, true, null, null, null);

                    listbox_playlist.SelectedIndex = listbox_playlist.SelectedIndex + 1;
                    listBox_index = listbox_playlist.SelectedIndex;

                    Play(listbox_playlist.Text);
                }
            }
            #region DE3UG
            catch (Exception exception)
            {
                Ex(exception);
            }
            #endregion
        }



        void PlayPrevious()
        {
            try
            {
                ignoreSkip = true;

                int currentindex = listbox_playlist.SelectedIndex;
                if (currentindex > 0)
                {
                    if (currentindex - 1 == 0)
                    {
                        ToggleButtons(null, null, null, false, null, null, null);
                    }
                    ToggleButtons(null, null, null, null, true, null, null);

                    listBox_index = listbox_playlist.SelectedIndex;
                    listbox_playlist.SelectedIndex = listbox_playlist.SelectedIndex - 1;
                    Play(listbox_playlist.Text);
                }
            }
            #region DE3UG
            catch (Exception exception)
            {
                Ex(exception);
            }
            #endregion
        }



        public void LoadLang()
        {
            config.Reload();
            Thread.Sleep(1000);
            lang.Load(config.lang);

            // => Controls:
            button_ytdownload.Text = lang.lang_main__button_ytdownload;
            checkbox_loop.Text = lang.lang_main__checkbox_loop;
            // => Menu:
            menu_file.Text = lang.lang_main__menu_file;
            menu_file_open.Text = lang.lang_main__menu_file_open;
            menu_file_exit.Text = lang.lang_main__menu_file_exit;
            menu_favorites.Text = lang.lang_main__menu_favorites;
            menu_other.Text = lang.lang_main__menu_other;
            menu_other_options.Text = lang.lang_main__menu_other_options;

            this.Refresh();
        }



        public void Ex(Exception _exception)
        {
            if (Scripts.config.debug == true)
            {
                Scripts.tools.Exception(_exception);
            }
        }



        public void ToggleButtons(bool? _play, bool? _pause, bool? _stop, bool? _previousTrack, bool? _nextTrack, bool? _backward, bool? _forward)
        {
            if (_play != null)
            {
                button_play.Enabled = (bool)_play;
            }
            if (_pause != null)
            {
                button_pause.Enabled = (bool)_pause;
            }
            if (_stop != null)
            {
                button_stop.Enabled = (bool)_stop;
            }

            if (_previousTrack != null)
            {
                button_previoustrack.Enabled = (bool)_previousTrack;
            }
            if (_nextTrack != null)
            {
                button_nexttrack.Enabled = (bool)_nextTrack;
            }

            if (_backward != null)
            {
                button_backward.Enabled = (bool)_backward;
            }
            if (_forward != null)
            {
                button_forward.Enabled = (bool)_forward;
            }
        }
        // # ================================================================================================================================= #
        #endregion

















        #region Load / Unload
        // # ================================================================================================================================= #
        public Main(bool _openWith, string _file)
        {
            try
            {
                InitializeComponent();
                ToggleButtons(false, false, false, false, false, false, false);
                trackbar_tempomusica.Enabled = false;
                this.Size = new Size(478, 284);

                combobox_ytformat.SelectedIndex = 0;

                checkbox_loop.Checked = config.loop;

                #region Load Lang
                if (config.lang == null || config.lang == "")
                {
                    config.lang = "en_EN";
                    config.Save();
                }

                LoadLang();
                #endregion Load Lang

                #region Load Volume Config
                float volume = config.volume;

                if (volume < 0 || volume > 1 || volume.ToString() == null)
                {
                    Scripts.jukebox.Volume = Convert.ToSingle(0.5);
                    config.volume = Convert.ToSingle(0.5);
                    config.Save();
                }
                else
                {
                    Scripts.jukebox.Volume = volume;
                }

                trackbar_volume.Value = Convert.ToInt32(Scripts.jukebox.Volume * 100);
                #endregion Load Voluem Config


                contextmenustrip.Visible = false;
                contextmenustrip.Items.Add(lang.lang_main__contextmenustrip_remove);

                #region Events
                this.DragEnter += new DragEventHandler(listbox_playlist_DragEnter);
                this.DragDrop += new DragEventHandler(listbox_playlist_DragDrop);

                Scripts.jukebox.PlaybackStopped += WaveOut_PlaybackStopped;
                trackbar_tempomusica.ValueChanged += trackBar_tempoMusica_Scroll;
                #endregion

                if (_openWith == true)
                {
                    Play(_file);
                    listbox_playlist.Items.Add(_file);
                    listbox_playlist.SelectedIndex = 0;
                }

                if (listbox_playlist.SelectedIndex + 1 == listbox_playlist.Items.Count)
                {
                    ToggleButtons(null, null, null, null, false, null, null);
                }
                ToggleButtons(null, null, null, false, null, null, null);
            }
            #region DE3UG
            catch (Exception exception)
            {
                Ex(exception);
            }
            #endregion DE3UG
        }



        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                config.volume = Scripts.jukebox.Volume;
                config.loop = checkbox_loop.Checked;
                config.Save();
            }
            #region DE3UG
            catch (Exception exception)
            {
                Ex(exception);
            }
            #endregion DE3UG
        }
        // # ================================================================================================================================= #
        #endregion Load / Unload



        #region Top Menu
        // # ================================================================================================================================= #
        private void menu_file_exit_Click(object sender, EventArgs e)
        {
            try
            {
                Environment.Exit(0);
            }
            #region DE3UG
            catch (Exception exception)
            {
                Ex(exception);
            }
            #endregion
        }



        private void menu_file_open_Click(object sender, EventArgs e)
        {
            try
            {
                filedialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.CommonMusic); filedialog.ShowDialog();
            }
            #region DE3UG
            catch (Exception exception)
            {
                Ex(exception);
            }
            #endregion
        }



        private void filedialog_FileOk(object sender, CancelEventArgs e)
        {
            try
            {
                if (filedialog.FileName != null)
                {
                    bool empty = false;

                    if (listbox_playlist.Items.Count == 0)
                    {
                        empty = true;
                    }

                    listbox_playlist.Items.Add(filedialog.FileName);
                    if (empty == true)
                    {
                        empty = false;
                        listbox_playlist.SelectedIndex = 0;
                    }

                    if (listbox_playlist.SelectedIndex < listbox_playlist.Items.Count - 1)
                    {
                        ToggleButtons(null, null, null, null, true, null, null);
                    }

                    if (Scripts.music.state != Scripts.Music.State.Playing)
                    {
                        Play(filedialog.FileName);
                    }
                }

                if (listbox_playlist.SelectedIndex >= 0)
                {
                    listbox_playlist.SelectedIndex = listbox_playlist.SelectedIndex;
                }
            }
            #region DE3UG
            catch (Exception exception)
            {
                Ex(exception);
            }
            #endregion
        }



        private void menu_other_options_Click(object sender, EventArgs e)
        {
            try
            {
                Options options = new Options();
                options.Show();
            }
            #region DE3UG
            catch (Exception exception)
            {
                Ex(exception);
            }
            #endregion
        }
        // # ================================================================================================================================= #
        #endregion Top Menu



        #region Botões
        // # ================================================================================================================================= #
        private void button_play_Click(object sender, EventArgs e)
        {
            try
            {
                Play(listbox_playlist.Text);
            }
            #region DE3UG
            catch (Exception exception)
            {
                Ex(exception);
            }
            #endregion
        }



        private void button_pause_Click(object sender, EventArgs e)
        {
            try
            {
                Pause();
            }
            #region DE3UG
            catch (Exception exception)
            {
                Ex(exception);
            }
            #endregion
        }



        private void button_stop_Click(object sender, EventArgs e)
        {
            try
            {
                Stop();
            }
            #region DE3UG
            catch (Exception exception)
            {
                Ex(exception);
            }
            #endregion
        }
        // # ================================================================================================================================= #



        #region Playlist
        // # ================================================================================================================================= #
        private void button_nexttrack_Click(object sender, EventArgs e)
        {
            try
            {
                PlayNext();
            }
            #region DE3UG
            catch (Exception exception)
            {
                Ex(exception);
            }
            #endregion
        }
        // # ================================================================================================================================= #



        // # ================================================================================================================================= #
        private void button_previoustrack_Click(object sender, EventArgs e)
        {
            try
            {
                PlayPrevious();
            }
            #region DE3UG
            catch (Exception exception)
            {
                Ex(exception);
            }
            #endregion
        }
        // # ================================================================================================================================= #
        #endregion Playlist



        #region TrackBar
        // # ================================================================================================================================= #
        private void button_backward_Click(object sender, EventArgs e)
        {
            try
            {
                int tenpercent = 0;

                tenpercent = (10 * trackbar_tempomusica.Maximum) / 100;

                if (trackbar_tempomusica.Value - tenpercent < trackbar_tempomusica.Minimum)
                {
                    trackbar_tempomusica.Value = trackbar_tempomusica.Minimum;
                }
                else
                {
                    trackbar_tempomusica.Value = trackbar_tempomusica.Value - tenpercent;
                }

                Scripts.music.audioFile.Position = trackbar_tempomusica.Value;
            }
            #region D3BUG
            catch (Exception exception)
            {
                Ex(exception);
            }
            #endregion
        }
        // # ================================================================================================================================= #



        // # ================================================================================================================================= #
        private void button_forward_Click(object sender, EventArgs e)
        {
            try
            {
                int tenpercent = 0;

                tenpercent = (10 * trackbar_tempomusica.Maximum) / 100;

                if (trackbar_tempomusica.Value + tenpercent > trackbar_tempomusica.Maximum)
                {
                    trackbar_tempomusica.Value = trackbar_tempomusica.Maximum;
                    Stop();
                }
                else
                {
                    trackbar_tempomusica.Value = trackbar_tempomusica.Value + tenpercent;
                }

                Scripts.music.audioFile.Position = trackbar_tempomusica.Value;
            }
            #region D3BUG
            catch (Exception exception)
            {
                Ex(exception);
            }
            #endregion
        }
        // # ================================================================================================================================= #
        #endregion TrackBar
        #endregion Botões



        #region Barra de Progresso ('trackBar_tempoMusica')
        // # ================================================================================================================================= #
        private void timer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (Scripts.music.state == Scripts.Music.State.Playing)
                {
                    if (Scripts.music.audioFile.Position <= trackbar_tempomusica.Maximum)
                    {
                        trackbar_tempomusica.Value = Convert.ToInt32(Scripts.music.audioFile.Position);
                    }
                }
            }
            #region D3BUG
            catch (Exception exception)
            {
                Ex(exception);
            }
            #endregion
        }



        private void trackBar_tempoMusica_Scroll(object sender, EventArgs e)
        {
            try
            {
                Scripts.music.audioFile.Position = trackbar_tempomusica.Value;
            }
            #region DE3UG
            catch (Exception exception)
            {
                Ex(exception);
            }
            #endregion
        }
        // # ================================================================================================================================= #
        #endregion



        #region Playlist (ListBox)
        // # ================================================================================================================================= #
        private void listBox_playlist_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    int index = listbox_playlist.IndexFromPoint(e.Location);
                    if (index != ListBox.NoMatches)
                    {
                        string selectedItem = listbox_playlist.Items[index].ToString();
                        listBox_item = listbox_playlist.Items[index];
                        contextmenustrip.Show(Cursor.Position);
                    }
                }
            }
            #region DE3UG
            catch (Exception exception)
            {
                Ex(exception);
            }
            #endregion
        }



        private void contextMenuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                if (e.ClickedItem.Text == "Remover")
                {
                    listbox_playlist.Items.Remove(listBox_item); listBox_item = null;
                }
            }
            #region DE3UG
            catch (Exception exception)
            {
                Ex(exception);
            }
            #endregion
        }



        private void listbox_playlist_Click(object sender, EventArgs e)
        {
            try
            {
                if (listbox_playlist.SelectedIndex != listBox_index)
                {
                    if (listbox_playlist.SelectedItem != null)
                    {
                        #region Toggle Buttons
                        int currentindex = listbox_playlist.SelectedIndex;
                        int maxindex = listbox_playlist.Items.Count - 1;

                        // Previous Track
                        if (currentindex - 1 < 0)
                        {
                            ToggleButtons(null, null, null, false, null, null, null);
                        }
                        else
                        {
                            ToggleButtons(null, null, null, true, null, null, null);
                        }
                        // Next Track
                        if (currentindex + 1 > maxindex)
                        {
                            ToggleButtons(null, null, null, null, false, null, null);
                        }
                        else
                        {
                            ToggleButtons(null, null, null, null, true, null, null);
                        }
                        #endregion Toggle Buttons

                        ignoreSkip = true;
                        listBox_index = listbox_playlist.SelectedIndex;
                        Play(listbox_playlist.Text);
                    }
                }
            }
            #region DE3UG
            catch (Exception exception)
            {
                Ex(exception);
            }
            #endregion
        }



        private void timer_playnext_Tick(object sender, EventArgs e)
        {
            try
            {
                PlayNext();
                timer_playnext.Stop();
            }
            #region DE3UG
            catch (Exception exception)
            {
                Ex(exception);
            }
            #endregion
        }
        // # ================================================================================================================================= #



        #region Drag'n'Drop
        // # ================================================================================================================================= #
        private void listbox_playlist_DragEnter(object sender, DragEventArgs e)
        {
            try
            {
                if (e.Data.GetDataPresent(DataFormats.FileDrop))
                {
                    e.Effect = DragDropEffects.Copy;
                }
            }
            #region DE3UG
            catch (Exception exception)
            {
                Ex(exception);
            }
            #endregion
        }
        // # ================================================================================================================================= #



        // # ================================================================================================================================= #
        private void listbox_playlist_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                listBox_index = listbox_playlist.SelectedIndex;
                bool select = false;
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                foreach (string file in files)
                {
                    bool empty = false;

                    if (Path.GetExtension(file) == ".mp3")
                    {
                        if (listbox_playlist.Items.Count == 0)
                        {
                            empty = true;
                        }

                        listbox_playlist.Items.Add(file);

                        if (empty == true)
                        {
                            empty = false;
                            listbox_playlist.SelectedIndex = 0;
                        }

                        int currentindex = listbox_playlist.SelectedIndex;
                        int maxindex = listbox_playlist.Items.Count - 1;

                        if (currentindex < maxindex)
                        {
                            button_nexttrack.Enabled = true;
                        }

                        if (Scripts.music.state != Scripts.Music.State.Playing)
                        {
                            listbox_playlist.SelectedIndex = listbox_playlist.Items.IndexOf(file);

                            if (listbox_playlist.Items.Count > 1)
                            {
                                button_previoustrack.Enabled = true;
                            }
                            button_nexttrack.Enabled = false;

                            string caminho = listbox_playlist.Text.Replace("\\", "\\\\");
                            Play(caminho);

                            select = false;
                        }
                        else
                        {
                            select = true;
                        }
                    }
                }

                if (listBox_index >= 0)
                {
                    if (select == true)
                    {
                        listbox_playlist.SelectedIndex = listBox_index;
                    }
                }
            }
            #region DE3UG
            catch (Exception exception)
            {
                Ex(exception);
            }
            #endregion
        }
        // # ================================================================================================================================= #
        #endregion
        // # ================================================================================================================================= #
        #endregion



        #region Volume
        // # ================================================================================================================================= #
        private void trackBar_volume_Scroll(object sender, EventArgs e)
        {
            try
            {
                float volume = trackbar_volume.Value;
                volume = volume / 100;

                try
                {
                    // => Error when not playing, doesn't affect anything.
                    Scripts.jukebox.Volume = volume;
                }
                catch { throw; }
            }
            #region DE3UG
            catch (Exception exception)
            {
                Ex(exception);
            }
            #endregion DE3UG
        }
        // # ================================================================================================================================= #
        #endregion Volume



        #region Loop
        // # ================================================================================================================================= #
        private void checkbox_loop_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                looping = checkbox_loop.Checked;
            }
            #region DE3UG
            catch (Exception exception)
            {
                Ex(exception);
            }
            #endregion
        }



        private void timer_loopcheck_Tick(object sender, EventArgs e)
        {
            try
            {
                if (doLoop == true)
                {
                    doLoop = false;
                    ToggleButtons(false, true, true, null, null, null, null);
                    Play(null);
                    timer_loopcheck.Stop();
                }
            }
            #region DE3UG
            catch (Exception exception)
            {
                Ex(exception);
            }
            #endregion DE3UG
        }
        // # ================================================================================================================================= #
        #endregion Loop



        #region YouTube Downloader
        // # ================================================================================================================================= #
        [DllImport("user32.dll")]
        static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);
        private const int CB_SETITEMHEIGHT = 0x153;

        private void SetComboBoxHeight(IntPtr comboBoxHandle, int comboBoxDesiredHeight)
        {
            try
            {
                SendMessage(comboBoxHandle, CB_SETITEMHEIGHT, -1, comboBoxDesiredHeight);
            }
            #region DE3UG
            catch (Exception exception)
            {
                Ex(exception);
            }
            #endregion
        }



        private void button_ytexpand_Click(object sender, EventArgs e)
        {
            try
            {
                if (combobox_ytformat.Height != 20)
                {
                    SetComboBoxHeight(combobox_ytformat.Handle, 14);
                    combobox_ytformat.Refresh();
                }

                if (ytExpanded != true)
                {
                    Size = new Size(478, 329);
                    button_ytexpand.Text = "▲";
                    ytExpanded = true;
                }
                else
                {
                    this.Size = new Size(478, 284);
                    button_ytexpand.Text = "▼";
                    ytExpanded = false;
                }
            }
            #region DE3UG
            catch (Exception exception)
            {
                Ex(exception);
            }
            #endregion
        }



        private async void button_ytdownload_Click(object sender, EventArgs e)
        {
            try
            {
                if (textbox_ytlink.Text != null && textbox_ytlink.Text != "")
                {
                    folderdialog.ShowDialog();
                    if (folderdialog.SelectedPath != null)
                    {
                        button_ytdownload.Enabled = false;
                        panel_ytDownloading.Visible = true;

                        await Scripts.tools.YouTubeDownloader(textbox_ytlink.Text, combobox_ytformat.Text, folderdialog.SelectedPath);

                        panel_ytDownloading.Visible = false;
                        button_ytdownload.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show(lang.lang_main__msgbox_ytnodirectory_text, "Bronze Player", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            #region DE3UG
            catch (Exception exception)
            {
                Ex(exception);
            }
            #endregion
        }
        // # ================================================================================================================================= #
        #endregion YouTube Downloader



        #region Favorites
        // # ================================================================================================================================= #
        private void menu_favorites_add_Click(object sender, EventArgs e)
        {
            try
            {
                if (listbox_playlist.Text != null && listbox_playlist.Text != "")
                {
                    var item = new ToolStripMenuItem { Text = listbox_playlist.Text };
                    item.Click += new EventHandler(favorites_Click);
                    menu_favorites.DropDownItems.Add(item);
                }
            }
            catch (Exception exception)
            {
                Ex(exception);
            }
        }



        private void favorites_Click(object sender, EventArgs e)
        {
            try
            {
                var item = (ToolStripMenuItem)sender;

                if (File.Exists(item.Text))
                {
                    Play(item.Text);
                }
            }
            catch (Exception exception)
            {
                Ex(exception);
            }
        }
        // # ================================================================================================================================= #
        #endregion Favorites










        #region WIP
        // # ================================================================================================================================= #
        private void timer_listentime_Tick(object sender, EventArgs e)
        {
            if (Scripts.music.state == Scripts.Music.State.Playing)
            {
                // Adiciona 1 segundo ao tempo total de reprodução na base de dados. =)
            }
        }
        // # ================================================================================================================================= #
        #endregion WIP
    }
}
