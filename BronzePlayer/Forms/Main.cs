#region Using

using System;
using System.Collections.Generic;
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

        private Config config = new Config();
        private Lang lang = new Lang();

        #endregion Refers

        #region Vars

        // # ================================================================================================================================= #
        private bool looping = false, doLoop = false, ytExpanded = false, ignoreSkip = false;

        private object listBox_item;
        private int listBox_index;

        public string version = "0.3.0", subVersion = "alpha";
        private List<string> ID = new List<string>();
        private List<string> Valor = new List<string>();
        private List<string> PlayList = new List<string>();

        // # ================================================================================================================================= #

        #endregion Vars

        #region Functions

        // # ================================================================================================================================= #
        private void Play(string _file)
        {
            try
            {
                trackbar_tempomusica.Enabled = true;
                ToggleButtons(false, true, true, null, null, true, true);

                Scripts.music.Play(_file);

                Text = "Bronze Player - " + Path.GetFileNameWithoutExtension(Scripts.music.audioFile.FileName);

                trackbar_tempomusica.Maximum = Convert.ToInt32(Scripts.music.audioFile.Length);
                trackbar_tempomusica.Value = Convert.ToInt32(Scripts.music.audioFile.Position);
                timer.Start();
            }

            #region DE3UG

            catch (Exception exception)
            {
                Scripts.tools.Exception(exception);
            }

            #endregion DE3UG
        }

        private void Pause()
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
                Scripts.tools.Exception(exception);
            }

            #endregion DE3UG
        }

        private void Stop()
        {
            try
            {
                ToggleButtons(true, false, false, null, null, false, false);

                trackbar_tempomusica.Maximum = 0;
                trackbar_tempomusica.Value = 0;
                trackbar_tempomusica.Enabled = false;

                Scripts.music.Stop();

                Text = "Bronze Player";

                timer.Stop();
            }

            #region DE3UG

            catch (Exception exception)
            {
                Scripts.tools.Exception(exception);
            }

            #endregion DE3UG
        }

        private void PlayNext()
        {
            try
            {
                ignoreSkip = true;

                var maxindex = listbox_playlist.Items.Count - 1;
                var currentindex = listbox_playlist.SelectedIndex;

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
                Scripts.tools.Exception(exception);
            }

            #endregion DE3UG
        }

        private void PlayPrevious()
        {
            try
            {
                ignoreSkip = true;

                var currentindex = listbox_playlist.SelectedIndex;
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
                Scripts.tools.Exception(exception);
            }

            #endregion DE3UG
        }

        public void LoadLang()
        {
            try
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
                menu_other_about.Text = lang.lang_main__menu_other_about;
                // => Labels:
                label_ytDownloading.Text = lang.lang_main__label_ytdownloading;

                Refresh();
            }

            #region DE3UG

            catch (Exception exception)
            {
                Scripts.tools.Exception(exception);
            }

            #endregion DE3UG
        }

        private void ToggleButtons(bool? _play, bool? _pause, bool? _stop, bool? _previousTrack, bool? _nextTrack, bool? _backward, bool? _forward)
        {
            try
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

            #region DE3UG

            catch (Exception exception)
            {
                Scripts.tools.Exception(exception);
            }

            #endregion DE3UG
        }

        private void LoadFavorites()
        {
            try
            {
                menu_favorites.DropDownItems.Clear();

                var menuItem1 = new ToolStripMenuItem { Text = lang.lang_main__menu_favorites_add };
                menuItem1.Click += menu_favorites_add_Click;
                menu_favorites.DropDownItems.Add(menuItem1);
                var menuItem2 = new ToolStripSeparator();
                menu_favorites.DropDownItems.Add(menuItem2);

                foreach (var id in Scripts.DataBase.Select("SELECT ID FROM Favoritos"))
                {
                    ID.Add(id);
                    var item = Scripts.tools.ReplaceWithCode(Scripts.DataBase.Select("SELECT Valor FROM Favoritos WHERE ID = " + id)[0].ToString(), Scripts.Tools.ReplaceType.Original);
                    Valor.Add(item);
                    AddFavoritesItem(Path.GetFileNameWithoutExtension(item));
                }
            }

            #region DE3UG

            catch (Exception exception)
            {
                Scripts.tools.Exception(exception);
            }

            #endregion DE3UG
        }

        private void AddFavoritesItem(string _name)
        {
            try
            {
                var item = new ToolStripMenuItem { Text = Scripts.tools.ReplaceWithCode(_name, Scripts.Tools.ReplaceType.Original) };
                item.MouseDown += new MouseEventHandler(favorites_Click);
                menu_favorites.DropDownItems.Add(item);
            }

            #region DE3UG

            catch (Exception exception)
            {
                Scripts.tools.Exception(exception);
            }

            #endregion DE3UG
        }

        // # ================================================================================================================================= #

        #endregion Functions

        #region Load / Unload

        // # ================================================================================================================================= #
        public Main(bool _openWith, string _file)
        {
            try
            {
                InitializeComponent();

                #region Load Lang

                if (!string.IsNullOrEmpty(config.lang))
                {
                    config.lang = "en_EN";
                    config.Save();
                }

                LoadLang();

                #endregion Load Lang

                ToggleButtons(false, false, false, false, false, false, false);
                trackbar_tempomusica.Enabled = false;
                Size = new Size(478, 284);

                LoadFavorites();

                combobox_ytformat.SelectedIndex = 0;

                checkbox_loop.Checked = config.loop;

                #region Load Volume Config

                var volume = config.volume;

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

                #endregion Load Volume Config

                contextmenustrip.Visible = false;
                contextmenustrip.Items.Add(lang.lang_main__contextmenustrip_remove);

                #region Events

                DragEnter += new DragEventHandler(listbox_playlist_DragEnter);
                DragDrop += new DragEventHandler(listbox_playlist_DragDrop);

                Scripts.jukebox.PlaybackStopped += WaveOut_PlaybackStopped;
                trackbar_tempomusica.ValueChanged += trackBar_tempoMusica_Scroll;

                #endregion Events

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
                Scripts.tools.Exception(exception);
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

                Scripts.music.Stop();

                Environment.Exit(0);
            }

            #region DE3UG

            catch (Exception exception)
            {
                Scripts.tools.Exception(exception);
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
                Scripts.tools.Exception(exception);
            }

            #endregion DE3UG
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
                Scripts.tools.Exception(exception);
            }

            #endregion DE3UG
        }

        private void filedialog_FileOk(object sender, CancelEventArgs e)
        {
            try
            {
                if (filedialog.FileName != null)
                {
                    if (!listbox_playlist.Items.Contains(filedialog.FileName))
                    {
                        listbox_playlist.Items.Add(filedialog.FileName);

                        if (listbox_playlist.Items.Count == 0)
                        {
                            listbox_playlist.SelectedIndex = 0;
                        }

                        if (listbox_playlist.SelectedIndex < listbox_playlist.Items.Count - 1)
                        {
                            ToggleButtons(null, null, null, null, true, null, null);
                        }

                        if (Scripts.music.state != Scripts.Music.State.Playing)
                        {
                            listbox_playlist.SelectedIndex = listbox_playlist.Items.IndexOf(filedialog.FileName);
                            Play(listbox_playlist.Text);
                        }
                    }
                    else
                    {
                        if (Scripts.music.state != Scripts.Music.State.Playing)
                        {
                            listbox_playlist.SelectedIndex = listbox_playlist.Items.IndexOf(filedialog.FileName);
                            Play(listbox_playlist.Text);
                        }
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
                Scripts.tools.Exception(exception);
            }

            #endregion DE3UG
        }

        private void menu_other_options_Click(object sender, EventArgs e)
        {
            try
            {
                var options = new Options();
                options.Show();
            }

            #region DE3UG

            catch (Exception exception)
            {
                Scripts.tools.Exception(exception);
            }

            #endregion DE3UG
        }

        private void menu_other_about_Click(object sender, EventArgs e)
        {
            var about = new About(Location);
            about.ShowDialog();
        }

        #region Favorites

        // # ================================================================================================================================= #
        private void menu_favorites_add_Click(object sender, EventArgs e)
        {
            try
            {
                if (listbox_playlist.Text != null && listbox_playlist.Text != "")
                {
                    var nome = Path.GetFileNameWithoutExtension(listbox_playlist.Text);
                    AddFavoritesItem(nome);
                    Scripts.DataBase.Insert("INSERT INTO Favoritos(Valor) VALUES('" + Scripts.tools.ReplaceWithCode(listbox_playlist.Text, Scripts.Tools.ReplaceType.Convert) + "')");
                    LoadFavorites();
                }
            }
            catch (Exception exception)
            {
                Scripts.tools.Exception(exception);
            }
        }

        private void favorites_Click(object sender, MouseEventArgs e)
        {
            try
            {
                var item = (ToolStripMenuItem)sender;
                var index = item.Owner.Items.IndexOf(item);
                var actualIndex = item.Owner.Items.IndexOf(item) - 2;

                if (e.Button == MouseButtons.Left)
                {
                    if (File.Exists(Valor[actualIndex].ToString()))
                    {
                        if (Valor[actualIndex].ToString() != listbox_playlist.Text)
                        {
                            if (!listbox_playlist.Items.Contains(Valor[actualIndex]))
                            {
                                listbox_playlist.Items.Add(Valor[actualIndex].ToString());
                                listbox_playlist.SelectedIndex = listbox_playlist.Items.Count - 1;
                            }
                            else
                            {
                                listbox_playlist.SelectedIndex = listbox_playlist.Items.IndexOf(Valor[actualIndex]);
                            }
                            ignoreSkip = true;
                            Play(Valor[actualIndex].ToString());
                        }
                        else
                        {
                            if (Scripts.music.state == Scripts.Music.State.Paused || Scripts.music.state == Scripts.Music.State.Stopped)
                            {
                                Play(null);
                            }
                        }
                    }
                }
                else if (e.Button == MouseButtons.Right)
                {
                    Scripts.dataBase.Delete("DELETE * FROM Favoritos WHERE ID = " + ID[actualIndex].ToString());

                    ID.RemoveAt(actualIndex);
                    Valor.RemoveAt(actualIndex);

                    menu_favorites.DropDownItems.RemoveAt(index);
                }
            }

            #region DE3UG

            catch (Exception exception)
            {
                Scripts.tools.Exception(exception);
            }

            #endregion DE3UG
        }

        // # ================================================================================================================================= #

        #endregion Favorites

        // # ================================================================================================================================= #

        #endregion Top Menu

        #region Botões

        // # ================================================================================================================================= #
        private void button_play_Click(object sender, EventArgs e)
        {
            try
            {
                if (Scripts.music.state == Scripts.Music.State.Stopped && Scripts.music.state == Scripts.Music.State.Null)
                {
                    Play(null);
                }
                else
                {
                    Play(listbox_playlist.Text);
                }
            }

            #region DE3UG

            catch (Exception exception)
            {
                Scripts.tools.Exception(exception);
            }

            #endregion DE3UG
        }

        private void button_pause_Click(object sender, EventArgs e)
        {
            try
            {
                ignoreSkip = true;
                Pause();
            }

            #region DE3UG

            catch (Exception exception)
            {
                Scripts.tools.Exception(exception);
            }

            #endregion DE3UG
        }

        private void button_stop_Click(object sender, EventArgs e)
        {
            try
            {
                ignoreSkip = true;
                Stop();
            }

            #region DE3UG

            catch (Exception exception)
            {
                Scripts.tools.Exception(exception);
            }

            #endregion DE3UG
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
                Scripts.tools.Exception(exception);
            }

            #endregion DE3UG
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
                Scripts.tools.Exception(exception);
            }

            #endregion DE3UG
        }

        // # ================================================================================================================================= #

        #endregion Playlist

        #region TrackBar

        // # ================================================================================================================================= #
        private void button_backward_Click(object sender, EventArgs e)
        {
            try
            {
                var tenpercent = 0;

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
                Scripts.tools.Exception(exception);
            }

            #endregion D3BUG
        }

        // # ================================================================================================================================= #

        // # ================================================================================================================================= #
        private void button_forward_Click(object sender, EventArgs e)
        {
            try
            {
                var tenpercent = 0;

                tenpercent = (10 * trackbar_tempomusica.Maximum) / 100;

                if (trackbar_tempomusica.Value + tenpercent > trackbar_tempomusica.Maximum)
                {
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
                Scripts.tools.Exception(exception);
            }

            #endregion D3BUG
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
                Scripts.tools.Exception(exception);
            }

            #endregion D3BUG
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
                Scripts.tools.Exception(exception);
            }

            #endregion DE3UG
        }

        // # ================================================================================================================================= #

        #endregion Barra de Progresso ('trackBar_tempoMusica')

        #region Playlist (ListBox)

        // # ================================================================================================================================= #
        private void listBox_playlist_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    var index = listbox_playlist.IndexFromPoint(e.Location);
                    if (index != System.Windows.Forms.ListBox.NoMatches)
                    {
                        var selectedItem = listbox_playlist.Items[index].ToString();
                        listBox_item = listbox_playlist.Items[index];
                        contextmenustrip.Show(Cursor.Position);
                    }
                }
            }

            #region DE3UG

            catch (Exception exception)
            {
                Scripts.tools.Exception(exception);
            }

            #endregion DE3UG
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
                Scripts.tools.Exception(exception);
            }

            #endregion DE3UG
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

                        var currentindex = listbox_playlist.SelectedIndex;
                        var maxindex = listbox_playlist.Items.Count - 1;

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
                Scripts.tools.Exception(exception);
            }

            #endregion DE3UG
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
                Scripts.tools.Exception(exception);
            }

            #endregion DE3UG
        }

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
                Scripts.tools.Exception(exception);
            }

            #endregion DE3UG
        }

        // # ================================================================================================================================= #

        // # ================================================================================================================================= #
        private void listbox_playlist_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                listBox_index = listbox_playlist.SelectedIndex;
                var select = false;
                var files = (string[])e.Data.GetData(DataFormats.FileDrop);
                foreach (var file in files)
                {
                    var empty = false;

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

                        var currentindex = listbox_playlist.SelectedIndex;
                        var maxindex = listbox_playlist.Items.Count - 1;

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

                            var caminho = listbox_playlist.Text.Replace("\\", "\\\\");
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
                Scripts.tools.Exception(exception);
            }

            #endregion DE3UG
        }

        // # ================================================================================================================================= #

        #endregion Drag'n'Drop

        // # ================================================================================================================================= #

        #endregion Playlist (ListBox)

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
                Scripts.tools.Exception(exception);
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
                Scripts.tools.Exception(exception);
            }

            #endregion DE3UG
        }

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
                            doLoop = true;
                            Invoke(new Action(() =>
                            {
                                timer_loopcheck.Start();
                            }));
                        }

                        #endregion Loop

                        else
                        {
                            var maxindex = listbox_playlist.Items.Count - 1;
                            var currentindex = listbox_playlist.SelectedIndex;

                            if (currentindex < maxindex)
                            {
                                Invoke(new Action(() =>
                                {
                                    timer_playnext.Start();
                                }));
                            }
                            else
                            {
                                Invoke(new Action(() =>
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
                Scripts.tools.Exception(exception);
            }

            #endregion DE3UG
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
                Scripts.tools.Exception(exception);
            }

            #endregion DE3UG
        }

        // # ================================================================================================================================= #

        #endregion Loop

        #region YouTube Downloader

        // # ================================================================================================================================= #
        [DllImport("user32.dll")]
        private static IntPtr SendMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);

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
                Scripts.tools.Exception(exception);
            }

            #endregion DE3UG
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
                    Size = new Size(478, 284);
                    button_ytexpand.Text = "▼";
                    ytExpanded = false;
                }
            }

            #region DE3UG

            catch (Exception exception)
            {
                Scripts.tools.Exception(exception);
            }

            #endregion DE3UG
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
                        label_ytDownloading.Visible = true;
                        progressbar_ytDownloading.Style = ProgressBarStyle.Marquee;

                        await Scripts.tools.YouTubeDownloader(textbox_ytlink.Text, combobox_ytformat.Text, folderdialog.SelectedPath);

                        progressbar_ytDownloading.Style = ProgressBarStyle.Blocks;
                        label_ytDownloading.Visible = false;
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
                Scripts.tools.Exception(exception);
            }

            #endregion DE3UG
        }

        // # ================================================================================================================================= #

        #endregion YouTube Downloader

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