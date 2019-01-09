using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
// => NuGet
using NAudio.Wave;

namespace BronzePlayer
{
    public partial class Main : Form
    {
        #region Refers
        // # ================================================================================================================================= #
        Config config = new Config();
        // # ================================================================================================================================= #
        #endregion



        #region Vars
        // # ================================================================================================================================= #
        bool playing = false, stopped = false, paused = false, looping = false, doloop = false, yt_expanded = false;
        long tempoMusica;
        string dirMusica, ytDownloadPath;
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
        void Reproduzir(string _fileDir)
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
                        paused = false; audioFile.Position = tempoMusica;
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
                    MessageBox.Show(exception.ToString(), "DE3UG - Reproduzir()", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            #endregion
        }
        // # ================================================================================================================================= #



        // # ================================================================================================================================= #
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

                        Parar();
                    }

                    if (currentindex + 1 == maxindex)
                    {
                        button_nexttrack.Enabled = false;
                    }
                    button_previoustrack.Enabled = true;

                    listbox_playlist.SelectedIndex = listbox_playlist.SelectedIndex + 1;
                    string caminho = listbox_playlist.SelectedItem.ToString().Replace("\\", "\\\\");
                    Reproduzir(caminho);
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
        // # ================================================================================================================================= #



        // # ================================================================================================================================= #
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
                        Parar();
                    }

                    if (currentindex - 1 == 0)
                    {
                        button_previoustrack.Enabled = false;
                    }
                    button_nexttrack.Enabled = true;

                    listbox_playlist.SelectedIndex = listbox_playlist.SelectedIndex - 1;
                    string caminho = listbox_playlist.SelectedItem.ToString().Replace("\\", "\\\\");
                    Reproduzir(caminho);
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
        // # ================================================================================================================================= #



        // # ================================================================================================================================= #
        void Pausar()
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
                    MessageBox.Show(exception.ToString(), "DE3UG - Pausar()", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            #endregion
        }
        // # ================================================================================================================================= #



        // # ================================================================================================================================= #
        void Parar()
        {
            try
            {
                button_forward.Enabled = false;
                button_backward.Enabled = false;

                stopped = true;
                playing = false; paused = false;
                trackbar_tempomusica.Maximum = 1;
                trackbar_tempomusica.Value = 0;

                timer.Stop();
                Scripts.waveOut.Dispose();
            }
            #region DE3UG
            catch (Exception exception)
            {
                if (config.debug == true)
                {
                    MessageBox.Show(exception.ToString(), "DE3UG - Parar()", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            #endregion
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
                float volume = config.volume;
                Scripts.waveOut.Volume = volume;
                trackbar_volume.Value = Convert.ToInt32(volume * 100);

                contextmenustrip.Visible = false;
                contextmenustrip.Items.Add("Remover");

                this.AllowDrop = true;

                #region Events
                this.DragEnter += new DragEventHandler(listbox_playlist_DragEnter);
                this.DragDrop += new DragEventHandler(listbox_playlist_DragDrop);

                Scripts.waveOut.PlaybackStopped += WaveOut_PlaybackStopped;
                trackbar_tempomusica.ValueChanged += trackBar_tempoMusica_Scroll;
                #endregion

                if (_openWith == true)
                {
                    Reproduzir(_fileDir);
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
        // # ================================================================================================================================= #



        // # ================================================================================================================================= #
        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                config.volume = Scripts.waveOut.Volume; config.loop = checkbox_loop.Checked;
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
        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
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
                    MessageBox.Show(exception.ToString(), "DE3UG - sairToolStripMenuItem_Click()", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            #endregion
        }
        // # ================================================================================================================================= #



        // # ================================================================================================================================= #
        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
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
                    MessageBox.Show(exception.ToString(), "DE3UG - abrirToolStripMenuItem_Click()", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            #endregion
        }
        // # ================================================================================================================================= #



        // # ================================================================================================================================= #
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
                        Reproduzir(fileDirectory);
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
        // # ================================================================================================================================= #
        #endregion



        #region Botões
        // # ================================================================================================================================= #
        private void button_play_Click(object sender, EventArgs e)
        {
            try
            {
                button_play.Enabled = false; button_pause.Enabled = true; button_stop.Enabled = true;
                var caminho = listbox_playlist.Text;
                caminho = caminho.Replace("\\", "\\\\");

                Reproduzir(caminho);
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
        // # ================================================================================================================================= #



        // # ================================================================================================================================= #
        private void button_pause_Click(object sender, EventArgs e)
        {
            try
            {
                button_pause.Enabled = false; button_play.Enabled = true;
                Pausar();
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
        // # ================================================================================================================================= #



        // # ================================================================================================================================= #
        private void button_stop_Click(object sender, EventArgs e)
        {
            try
            {
                button_play.Enabled = true; button_pause.Enabled = false; button_stop.Enabled = false;
                Parar();
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
                    Parar();
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
        // # ================================================================================================================================= #



        // # ================================================================================================================================= #
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
        // # ================================================================================================================================= #



        // # ================================================================================================================================= #
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
        // # ================================================================================================================================= #



        // # ================================================================================================================================= #
        private void listbox_playlist_Click(object sender, EventArgs e)
        {
            try
            {
                if (listbox_playlist.SelectedIndex != listBox_index)
                {
                    if (playing == true)
                    {
                        Parar();
                    }

                    string caminho = listbox_playlist.SelectedItem.ToString().Replace("\\", "\\\\");
                    Reproduzir(caminho);
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
        // # ================================================================================================================================= #



        // # ================================================================================================================================= #
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
                bool tocar = false; bool select = false;
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


                        tocar = true;
                    }
                }

                if (tocar == true)
                {
                    if (playing == false)
                    {
                        select = false; listbox_playlist.SelectedIndex = 0; string caminho = listbox_playlist.Text.Replace("\\", "\\\\"); Reproduzir(caminho);
                    }
                    else
                    {
                        select = true;
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

                Scripts.waveOut.Volume = x;
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
        // # ================================================================================================================================= #



        // # ================================================================================================================================= #
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

                    Reproduzir(caminho);
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
        static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, Int32 wParam, Int32 lParam);
        private const Int32 CB_SETITEMHEIGHT = 0x153;

        private void SetComboBoxHeight(IntPtr comboBoxHandle, Int32 comboBoxDesiredHeight)
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
        // # ================================================================================================================================= #



        // # ================================================================================================================================= #
        private void textbox_ytlink_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (textbox_ytlink.Text.Length > 0)
                {
                    button_ytdownload.Enabled = true;
                }
                else
                {
                    button_ytdownload.Enabled = false;
                }
            }
            #region DE3UG
            catch (Exception exception)
            {
                if (config.debug == true)
                {
                    MessageBox.Show(exception.ToString(), "textbox_ytlink_TextChanged()", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            #endregion
        }
        // # ================================================================================================================================= #



        // # ================================================================================================================================= #
        private void button_ytdownload_Click(object sender, EventArgs e)
        {
            try
            {
                folderdialog.ShowDialog();
                if (folderdialog.SelectedPath != null)
                {
                    ytDownloadPath = folderdialog.SelectedPath;
                    button_ytdownload.Enabled = false;
                    background_ytdownloading.RunWorkerAsync();
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



        // # ================================================================================================================================= #
        private void button_ytexpand_Click(object sender, EventArgs e)
        {
            try
            {
                if (combobox_ytformat.Height != 20)
                {
                    SetComboBoxHeight(combobox_ytformat.Handle, 14);
                    combobox_ytformat.Refresh();

                    button_ytdownload.Enabled = false;
                }

                if (yt_expanded != true)
                {
                    this.Size = new Size(478, 315);
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
        // # ================================================================================================================================= #



        // # ================================================================================================================================= #
        private void background_ytdownloading_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                Scripts.tools.YouTubeDownloader(textbox_ytlink.Text, ytDownloadPath, combobox_ytformat.Text);
                ytDownloadPath = null;
            }
            #region DE3UG
            catch (Exception exception)
            {
                if (config.debug == true)
                {
                    MessageBox.Show(exception.ToString(), "background_ytdownloading_DoWork()", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            #endregion
        }
        // # ================================================================================================================================= #



        // # ================================================================================================================================= #
        private void background_ytdownloading_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                button_ytdownload.Enabled = true;
                MessageBox.Show("Download complete!");
            }
            #region DE3UG
            catch (Exception exception)
            {
                if (config.debug == true)
                {
                    MessageBox.Show(exception.ToString(), "background_ytdownloading_RunWorkerCompleted()", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            #endregion
        }
        // # ================================================================================================================================= #
        #endregion YouTube Downloader
    }
}