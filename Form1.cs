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

            ///This loop helps to fill toFill variable with 10 characters of A-Z
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


            var values = File.ReadAllLines("words_alpha.txt").ToList();
            var array = txtCharacter.Text.ToLower().OrderBy(x => x).ToArray();

            ///For every word in the dictionary....
            foreach (var words in values)
            {
                ///Breaks the word into a character array, then reorder them by alphabetical
                ///order
                var wordArray = words.OrderBy(x => x).ToArray();

                ///Checks if the word in the dictionary is longer than the input
                if (words.Length < txtCharacter.Text.Length)
                {
                    ///Initialise bool check to be true by default
                    var boolCheck = true;

                    ///Distinctify the characters of the current word in the dictionary
                    var hashWord = wordArray.Distinct();

                    foreach (var item in hashWord)
                    {
                        ///Checks for every new character for comparison, if bool check is false. If so, break the loop
                        ///and skip to the next word in the dictionary
                        if (!boolCheck) break;
                        else
                        {
                            ///Checks if the input contains the word's character, else bool check will be false,
                            ///breaking the loop and skipping the word in the dictionary
                            if (!array.Contains(item))
                            {
                                boolCheck = false;
                            }
                            else
                            {
                                ///Checks if the current character, if it is more than the same character of the input,
                                ///change boolean check to be false, breaking the loop and not adding that specific word
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

                    ///If boolean check is still true after every check, add the word into the results list
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

                    ///Index increase based on the length of the input character array
                    for (int i = 0; i < array.Length; i++)
                    {
                        var item = array[i];

                        ///Checks for every new character for comparison, if bool check is false. If so, break the loop
                        ///and skip to the next word in the dictionary
                        if (!boolCheck) break;
                        else
                        {
                            ///Checks if the word's character and input character (since ordered by alphabetical order) matches.
                            ///If it doesnt, change boolean check to be false, breaking the loop and not adding that specific word
                            if (wordArray[i] != item)
                            {
                                boolCheck = false;
                            }

                            ///Else remain true and eventually add the word into the results
                            else
                            {
                                boolCheck = true;
                            }


                        }
                    }

                    ///If boolean check is still true after every check, add the word into the results list
                    if (boolCheck)
                    {
                        lbResults.Items.Add(words);
                    }

                }
            }






        }
    }
}
