

using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;
using System.Text;
namespace hangman;

public partial class MainPage : ContentPage
{
    private string word = "hello";

    public MainPage()
    {
        InitializeComponent();
    }

    private int incorrectGuesses = 0;
    private void OnButtonClicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var letter = button.Text.ToLower();

        if (word.Contains(letter))
        {
            button.IsEnabled = false;

            // Update the wordLabel text with the correct letter in the blank space(s)
            var sb = new StringBuilder(wordLabel.Text);
            for (int i = 0; i < word.Length; i++)
            {
                if (word[i].ToString().ToLower() == letter)
                {
                    sb[i * 2] = word[i];
                }
            }
            wordLabel.Text = sb.ToString();

            // Display "Correct" message
            DisplayAlert("Result", "Correct", "OK");

            if (!wordLabel.Text.Contains('_'))
            {
                DisplayAlert("Congratulations", "the word was hello!", "OK");
                ResetGame();
            }
        }
        else
        {
            button.IsEnabled = false;
            incorrectGuesses++;

            if (incorrectGuesses <= 6)
            {
                var imageName = $"hangman{incorrectGuesses}.jpg";
                hangmanImage.Source = imageName;
            }
            // Display "Wrong" message
            DisplayAlert("Result", "Wrong", "OK");
            if (incorrectGuesses >= 5)
            {
                DisplayAlert("YOU LOST", "boo hoo", "OK");
                ResetGame();
            }
        }


    }
    private void ResetGame()
    {
        // Reset the buttons
        foreach (var btn in buttonsPanel.Children)
        {
            var b = (Button)btn;
            b.IsEnabled = true;
        }

        // Reset the hangman image
        hangmanImage.Source = "hangman0.jpg";

        // Reset the view model

        incorrectGuesses = 0;

        // Reset the wordLabel text
        var sb = new StringBuilder();
        foreach (var letter in wordLabel.Text.Take(5))
        {
            sb.Append("_ ");
        }
        wordLabel.Text = sb.ToString();
    }


}

