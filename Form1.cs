using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnagramApplication
{
    public partial class Form1 : Form
    {
        //Initial Commit
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// This method generates 10 random characters into the textbox when clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRandom_Click(object sender, EventArgs e)
        {
            txtCharacter.Text = string.Empty;
            txtCharacter.Text += randomString();
        }

        /// <summary>
        /// This method is only called when event btnRandom_Click() is triggered
        /// </summary>
        /// <returns></returns>
        private string randomString()
        {
            Random random = new Random();
            const string x = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var toFill = string.Empty;
            for (int i = 0; i < 10; i++)
            {
                toFill += x.Select(s => x[random.Next(x.Length)]).FirstOrDefault();
            }

            return toFill;
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            lbResults.Items.Clear();
            using (var streamReader = new StreamReader("C:/Users/Asus/source/repos/AnagramApplication/words_alpha.txt"))
            {

                var values = streamReader.ReadToEnd();
                var list = values.Split('\n').ToList();
                var listForComparison = new List<string>();


                foreach (var words in list)
                {
                    if (words.Length > txtCharacter.Text.Length)
                    {
                        listForComparison.Add(words);
                    }

                    ///TO FIX: Use ordinary For loop
                    else if (words.Length < txtCharacter.Text.Length)
                    {
                        var boolCheck = true;
                        foreach (var wordA in words.ToCharArray().OrderBy(x => x))
                        {
                            if (!boolCheck) break;
                            else
                            {
                                foreach (var item in txtCharacter.Text.ToLower().ToCharArray().OrderBy(x => x))
                                {
                                    if (wordA != item)
                                    {
                                        boolCheck = false;
                                        break;
                                    }
                                    else
                                    {
                                        boolCheck = true;
                                        break;
                                    }
                                }
                            }



                        }
                        if (boolCheck == false)
                        {
                            listForComparison.Add(words);
                        }

                    }

                    ///TO FIX: Use ordinary For loop
                    else if (words.Length == txtCharacter.Text.Length)
                    {
                        var boolCheck = true;
                        foreach (var item in txtCharacter.Text.ToLower().ToCharArray().OrderBy(x => x))
                        {
                            if (!boolCheck) break;
                            else
                            {
                                foreach (var wordA in words.ToCharArray().OrderBy(x => x))
                                {
                                    if (wordA != item)
                                    {
                                        boolCheck = false;
                                        break;
                                    }
                                    else
                                    {
                                        boolCheck = true;
                                    }
                                }

                            }

                        }
                        if (boolCheck == false)
                        {
                            listForComparison.Add(words);
                        }

                    }
                }


                foreach (var item in listForComparison)
                {
                    list.Remove(item);
                }

                lbResults.Items.AddRange(list.ToArray());





            }
        }
    }
}
