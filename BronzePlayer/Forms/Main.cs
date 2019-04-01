using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Runtime.InteropServices;
// => NuGet
using NAudio.Wave; // NAudio
using NYoutubeDL;
// => Projects
using BronzePlayer.Forms;
using System.Linq;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.Generic;

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
        bool playing = false, stopped = false, paused = false, looping = false, doloop = false, yt_expanded = false;
        long tempoMusica;
        string dirMusica;
        object listBox_item;
        int listBox_index;
        // # ================================================================================================================================= #



        // # ================================================================================================================================= #
        AudioFileReader audioFile;
        // # ================================================================================================================================= #
        #endregion



        #region Events
        // # ================================================================================================================================= #
        private void WaveOut_PlaybackStopped(object sender, StoppedEventArgs e)
        {
            try
            {
                dirMusica = audioFile.FileName;
                if (playing == true && paused == false && stopped == false)
                {
                    timer.Stop();
                    playing = false; paused = false; stopped = true;
                    #region Loop
                    if (looping == true)
                    {
                        this.Invoke(new Action(() =>
                        {
                            timer_loopcheck.Start();
                        }));
                        doloop = true;
                    }
                    #endregion Loop
                    else
                    {
                        int maxindex = listbox_playlist.Items.Count - 1; int currentindex = listbox_playlist.SelectedIndex;
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
                                button_play.Enabled = true;
                                button_pause.Enabled = false;
                                button_stop.Enabled = false;
                            }));
                        }
                    }
                }
            }
            #region DE3UG
            catch (Exception exception)
            {
                if (config.debug == true)
                {
                    MessageBox.Show(exception.ToString(), "DE3UG - WaveOut_PlaybackStopped()", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            #endregion
        }
        // # ================================================================================================================================= #
        #endregion



        #region Functions
        // # ================================================================================================================================= #
        void Play(string _fileDir)
        {
            try
            {
                if (playing == false)
                {
                    button_forward.Enabled = true;
                    button_backward.Enabled = true;

                    if (paused == false && stopped == false)
                    {
                        if (_fileDir != null)
                        {
                            audioFile = new AudioFileReader(_fileDir); stopped = false; paused = false;
                        }
                    }
                    else
                    {
                        paused = false;
                        audioFile.Position = tempoMusica;
                    }

                    if (stopped == true)
                    {
                        if (_fileDir != null)
                        {
                            stopped = false;
                            audioFile = new AudioFileReader(_fileDir);
                            dirMusica = _fileDir;
                        }
                        else
                        {
                            stopped = false;
                            audioFile = new AudioFileReader(dirMusica);
                        }
                    }

                    string musicName;
                    musicName = Path.GetFileNameWithoutExtension(audioFile.FileName);
                    this.Text = "Bronze Player - " + musicName;

                    playing = true;
                    Scripts.waveOut.Init(audioFile); Scripts.waveOut.Play();
                    trackbar_tempomusica.Maximum = Convert.ToInt32(audioFile.Length); trackbar_tempomusica.Value = Convert.ToInt32(audioFile.Position); timer.Start();
                }
            }
            #region DE3UG
            catch (Exception exception)
            {
                if (config.debug == true)
                {
                    MessageBox.Show(exception.ToString(), "DE3UG - Play()", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            #endregion
        }
        

        void PlayNext()
        {
            try
            {
                int maxindex = listbox_playlist.Items.Count - 1; int currentindex = listbox_playlist.SelectedIndex;
                if (currentindex < maxindex)
                {
                    if (playing == true)
                    {
                        playing = false;
                        paused = false;
                        stopped = true;

                        Stop();
                    }

                    if (currentindex + 1 == maxindex)
                    {
                        button_nexttrack.Enabled = false;
                    }
                    button_previoustrack.Enabled = true;

                    listbox_playlist.SelectedIndex = listbox_playlist.SelectedIndex + 1;
                    string caminho = listbox_playlist.SelectedItem.ToString().Replace("\\", "\\\\");
                    Play(caminho);
                }
            }
            #region DE3UG
            catch (Exception exception)
            {
                if (config.debug == true)
                {
                    MessageBox.Show(exception.ToString(), "DE3UG - PlayNext()", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            #endregion
        }
        


        void PlayPrevious()
        {
            try
            {
                int currentindex = listbox_playlist.SelectedIndex;
                if (currentindex > 0)
                {
                    if (playing == true)
                    {
                        playing = false;
                        Stop();
                    }

                    if (currentindex - 1 == 0)
                    {
                        button_previoustrack.Enabled = false;
                    }
                    button_nexttrack.Enabled = true;

                    listbox_playlist.SelectedIndex = listbox_playlist.SelectedIndex - 1;
                    string caminho = listbox_playlist.SelectedItem.ToString().Replace("\\", "\\\\");
                    Play(caminho);
                }
            }
            #region DE3UG
            catch (Exception exception)
            {
                if (config.debug == true)
                {
                    MessageBox.Show(exception.ToString(), "DE3UG - PlayPrevious()", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            #endregion
        }
        


        void Pause()
        {
            try
            {
                button_forward.Enabled = false;
                button_backward.Enabled = false;

                tempoMusica = audioFile.Position;
                paused = true;
                playing = false; stopped = false;
                timer.Stop();
                Scripts.waveOut.Dispose();
            }
            #region DE3UG
            catch (Exception exception)
            {
                if (config.debug == true)
                {
                    MessageBox.Show(exception.ToString(), "DE3UG - Pause()", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            #endregion
        }
        


        void Stop()
        {
            try
            {
                button_forward.Enabled = false;
                button_backward.Enabled = false;

                stopped = true;
                playing = false; paused = false;
                trackbar_tempomusica.Maximum = 1;
                trackbar_tempomusica.Value = 0;

                this.Text = "Bronze Player";

                timer.Stop();
                Scripts.waveOut.Dispose();
            }
            #region DE3UG
            catch (Exception exception)
            {
                if (config.debug == true)
                {
                    MessageBox.Show(exception.ToString(), "DE3UG - Stop()", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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
        // # ================================================================================================================================= #
        #endregion















        #region Load / Unload
        // # ================================================================================================================================= #
        public Main(bool _openWith, string _fileDir)
        {
            try
            {
                InitializeComponent();

                this.Size = new Size(478, 284);

                combobox_ytformat.SelectedIndex = 0;

                button_forward.Enabled = false;
                button_backward.Enabled = false;

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
                    Scripts.waveOut.Volume = Convert.ToSingle(0.5);
                    config.volume = Convert.ToSingle(0.5);
                    config.Save();
                }
                else
                {
                    Scripts.waveOut.Volume = volume;
                }

                trackbar_volume.Value = Convert.ToInt32(Scripts.waveOut.Volume * 100);
                #endregion Load Voluem Config


                contextmenustrip.Visible = false;
                contextmenustrip.Items.Add(lang.lang_main__contextmenustrip_remove);

                this.AllowDrop = true;

                #region Events
                this.DragEnter += new DragEventHandler(listbox_playlist_DragEnter);
                this.DragDrop += new DragEventHandler(listbox_playlist_DragDrop);

                Scripts.waveOut.PlaybackStopped += WaveOut_PlaybackStopped;
                trackbar_tempomusica.ValueChanged += trackBar_tempoMusica_Scroll;
                #endregion

                if (_openWith == true)
                {
                    Play(_fileDir);
                    listbox_playlist.Items.Add(_fileDir);
                    listbox_playlist.SelectedIndex = 0;
                    dirMusica = _fileDir;
                    button_play.Enabled = false;
                }


                int currentindex = listbox_playlist.SelectedIndex;
                int maxindex = listbox_playlist.Items.Count;

                if (currentindex + 1 == maxindex)
                {
                    button_nexttrack.Enabled = false;
                }
                button_previoustrack.Enabled = false;
            }
            #region DE3UG
            catch (Exception exception)
            {
                if (config.debug == true)
                {
                    MessageBox.Show(exception.ToString(), "DE3UG - Main()", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            #endregion DE3UG
        }
        


        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                config.volume = Scripts.waveOut.Volume;
                config.loop = checkbox_loop.Checked;
                config.Save();
            }
            #region DE3UG
            catch (Exception exception)
            {
                if (config.debug == true)
                {
                    MessageBox.Show(exception.ToString(), "DE3UG - Main_FormClosing()", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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
                this.Close();
            }
            #region DE3UG
            catch (Exception exception)
            {
                if (config.debug == true)
                {
                    MessageBox.Show(exception.ToString(), "DE3UG - menu_file_exit_Click()", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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
                if (config.debug == true)
                {
                    MessageBox.Show(exception.ToString(), "DE3UG - menu_file_open_Click()", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            #endregion
        }


        private void filedialog_FileOk(object sender, CancelEventArgs e)
        {
            try
            {
                int lastIndex = listbox_playlist.SelectedIndex;

                string fileDirectory = filedialog.FileName;
                if (fileDirectory != null)
                {
                    bool empty = false;

                    audioFile = new AudioFileReader(fileDirectory);

                    if (listbox_playlist.Items.Count == 0)
                    {
                        empty = true;
                    }

                    listbox_playlist.Items.Add(fileDirectory);
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

                    if (playing == false)
                    {
                        Play(fileDirectory);
                    }
                }

                if (lastIndex >= 0)
                {
                    listbox_playlist.SelectedIndex = lastIndex;
                }
            }
            #region DE3UG
            catch (Exception exception)
            {
                if (config.debug == true)
                {
                    MessageBox.Show(exception.ToString(), "DE3UG - filedialog_FileOk()", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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
                if (config.debug == true)
                {
                    MessageBox.Show(exception.ToString(), "DE3UG - menu_other_options_Click()", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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
                button_play.Enabled = false; button_pause.Enabled = true; button_stop.Enabled = true;
                var caminho = listbox_playlist.Text;
                caminho = caminho.Replace("\\", "\\\\");

                Play(caminho);
            }
            #region DE3UG
            catch (Exception exception)
            {
                if (config.debug == true)
                {
                    MessageBox.Show(exception.ToString(), "DE3UG - button_play_Click()", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            #endregion
        }
        


        private void button_pause_Click(object sender, EventArgs e)
        {
            try
            {
                button_pause.Enabled = false; button_play.Enabled = true;
                Pause();
            }
            #region DE3UG
            catch (Exception exception)
            {
                if (config.debug == true)
                {
                    MessageBox.Show(exception.ToString(), "button_pause_Click()", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            #endregion
        }
        


        private void button_stop_Click(object sender, EventArgs e)
        {
            try
            {
                button_play.Enabled = true; button_pause.Enabled = false; button_stop.Enabled = false;
                Stop();
            }
            #region DE3UG
            catch (Exception exception)
            {
                if (config.debug == true)
                {
                    MessageBox.Show(exception.ToString(), "DE3UG - button_stop_Click()", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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
                if (config.debug == true)
                {
                    MessageBox.Show(exception.ToString(), "DE3UG - button_nexttrack_Click()", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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
                if (config.debug == true)
                {
                    MessageBox.Show(exception.ToString(), "DE3UG - button_previoustrack_Click()", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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

                audioFile.Position = trackbar_tempomusica.Value;
            }
            #region D3BUG
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString(), "DE3UG - button_backward_Click()", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                audioFile.Position = trackbar_tempomusica.Value;
            }
            #region D3BUG
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString(), "DE3UG - button_forward_Click()", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                if (playing == true)
                {
                    if (audioFile.Position <= trackbar_tempomusica.Maximum)
                    {
                        trackbar_tempomusica.Value = Convert.ToInt32(audioFile.Position);
                    }
                }
            }
            #region D3BUG
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString(), "DE3UG - timer_Tick()", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            #endregion
        }
        


        private void trackBar_tempoMusica_Scroll(object sender, EventArgs e)
        {
            try
            {
                audioFile.Position = trackbar_tempomusica.Value;
            }
            #region DE3UG
            catch (Exception exception)
            {
                if (config.debug == true)
                {
                    MessageBox.Show(exception.ToString(), "DE3UG - trackBar_tempoMusica_Scroll()", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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
                if (config.debug == true)
                {
                    MessageBox.Show(exception.ToString(), "DE3UG - listBox_playlist_MouseDown()", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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
                if (config.debug == true)
                {
                    MessageBox.Show(exception.ToString(), "DE3UG - contextMenuStrip_ItemClicked()", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            #endregion
        }
        


        private void listbox_playlist_Click(object sender, EventArgs e)
        {
            try
            {
                if (listbox_playlist.SelectedIndex != listBox_index)
                {
                    if (playing == true)
                    {
                        Stop();
                    }

                    if (listbox_playlist.SelectedItem != null)
                    {
                        string caminho = listbox_playlist.SelectedItem.ToString().Replace("\\", "\\\\");
                        Play(caminho);
                    }
                }
            }
            #region DE3UG
            catch (Exception exception)
            {
                if (config.debug == true)
                {
                    MessageBox.Show(exception.ToString(), "DE3UG - listbox_playlist_Click()", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            #endregion
        }
        


        private void timer_playnext_Tick(object sender, EventArgs e)
        {
            try
            {
                PlayNext(); timer_playnext.Stop();
            }
            #region DE3UG
            catch (Exception exception)
            {
                if (config.debug == true)
                {
                    MessageBox.Show(exception.ToString(), "DE3UG - timer_playnext_Tick()", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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
                if (config.debug == true)
                {
                    MessageBox.Show(exception.ToString(), "DE3UG - listbox_playlist_DragEnter", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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

                        if (playing == false)
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
                if (config.debug == true)
                {
                    MessageBox.Show(exception.ToString(), "DE3UG - listbox_playlist_DragDrop()", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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
                float x = trackbar_volume.Value;
                x = x / 100;

                try
                {
                    // => Error when not playing, doesn't affect anything.
                    Scripts.waveOut.Volume = x;
                }
                catch
                {
                    throw;
                }
            }
            #region DE3UG
            catch (Exception exception)
            {
                if (config.debug == true)
                {
                    MessageBox.Show(exception.ToString(), "DE3UG - trackBar_volume_Scroll()", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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
                if (config.debug == true)
                {
                    MessageBox.Show(exception.ToString(), "checbox_loop_CheckedChanged()", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            #endregion
        }



        private void menu_playlists_Click(object sender, EventArgs e)
        {
            string appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string playlistsDir = appdata + @"\Milkenm\Bronze Player\Playlists\";
            if (!Directory.Exists(playlistsDir)) // If NOT exists.
            {
                Directory.CreateDirectory(playlistsDir);
            }

            foreach (var folder in Directory.GetDirectories(playlistsDir))
            {
                menu_favorites.DropDownItems.Add(folder);
            }
        }
        


        private void timer_loopcheck_Tick(object sender, EventArgs e)
        {
            try
            {
                if (doloop == true)
                {
                    doloop = false;
                    button_play.Enabled = false; button_pause.Enabled = true; button_stop.Enabled = true;
                    var caminho = dirMusica;
                    caminho = caminho.Replace("\\", "\\\\");

                    Play(caminho);
                    timer_loopcheck.Stop();
                }
            }
            #region DE3UG
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString(), "DE3UG - timer_loopcheck_Tick()", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                if (config.debug == true)
                {
                    MessageBox.Show(exception.ToString(), "SetComboBoxHeight()", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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

                if (yt_expanded != true)
                {
                    Size = new Size(478, 329);
                    button_ytexpand.Text = "▲";
                    yt_expanded = true;
                }
                else
                {
                    this.Size = new Size(478, 284);
                    button_ytexpand.Text = "▼";
                    yt_expanded = false;
                }
            }
            #region DE3UG
            catch (Exception exception)
            {
                if (config.debug == true)
                {
                    MessageBox.Show(exception.ToString(), "button_ytexpand_Click()", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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
                if (config.debug == true)
                {
                    MessageBox.Show(exception.ToString(), "button_ytdownload_Click()", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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
                    var item = new ToolStripMenuItem();
                    item.Text = listbox_playlist.Text;
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
                var name = item.Text;

                if (File.Exists(name))
                {
                    Play(name);
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
            if (playing == true)
            {
                // Adiciona 1 segundo ao tempo total de reprodução na base de dados. =)
            }
        }
        // # ================================================================================================================================= #
        #endregion WIP
    }
}
