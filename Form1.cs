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

        /// <summary>
        /// This method is triggered when the Generate button is triggered
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGenerate_Click(object sender, EventArgs e)
        {
            lbResults.Items.Clear();
            using (var streamReader = new StreamReader("D:/Users/Dylan Koh/Source/Repos/DylanKoh/AnagramApplication/words_alpha.txt"))
            {

                var values = streamReader.ReadToEnd();
                var stringSeperate = new string[] { "\r\n" };
                var list = values.Split(stringSeperate, StringSplitOptions.None).ToList();
                var array = txtCharacter.Text.ToLower().OrderBy(x => x).ToArray();

                ///For every word in the dictionary....
                foreach (var words in list)
                {
                    ///Breaks the word into a character array, then reorder them by alphabetical
                    ///order
                    var wordArray = words.OrderBy(x => x).ToArray();

                    ///Checks if the word in the dictionary is longer than the input
                    if (words.Length < txtCharacter.Text.Length)
                    {
                        var boolCheck = true;
                        var hashWord = wordArray.Distinct();

                        foreach (var item in hashWord)
                        {
                            if (!boolCheck) break;
                            else
                            {

                                if (!array.Contains(item))
                                {
                                    boolCheck = false;
                                }
                                else
                                {
                                    if (array.Where(x => x == item).Select(x => x).Count() < wordArray.Where(x => x == item).Select(x => x).Count())
                                    { 
                                        boolCheck = false;
                                    }
                                    else
                                    {
                                        boolCheck = true;
                                    }
                                    
                                }


                            }
                        }

                        ///If boolean check is true, add the word into the results list
                        if (boolCheck)
                        {
                            lbResults.Items.Add(words);
                        }

                    }

                    ///Checks if the word in the dictionary is as long as the input
                    else if (words.Length == txtCharacter.Text.Length)
                    {
                        ///Initiate boolean check value
                        var boolCheck = true;

                        ///Index increase base on the length of the array
                        for (int i = 0; i < array.Length; i++)
                        {
                            var item = array[i];
                            if (!boolCheck) break;
                            else
                            {

                                if (wordArray[i] != item)
                                {
                                    boolCheck = false;
                                }
                                else
                                {
                                    boolCheck = true;
                                }


                            }
                        }
                        if (boolCheck)
                        {
                            lbResults.Items.Add(words);
                        }

                    }
                }





            }
        }
    }
}
