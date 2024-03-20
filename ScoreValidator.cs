using System;

public static class ScoreValidator
{
    //TODO: CUA, if user leaves textbox call this method 'ValidateTotalItems()' and use EventHandler()
    // Note: for Items only

    /*
        private void TextBox_Leave(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            string textBoxName = textBox.Name;
            string input = textBox.Text;
            ValidateTotalItems(input, name);
        }
    */

    public static string userInput_score = "40"; // TextBox
    public static string userInput_items = "50"; // TexBox
    public static string activityType = "LabActItems1"; //Variable of Texbox

    public static void Main(string[] args)
    {
        ValidateTotalItems(userInput_items, activityType);

        //TODO: when user clicked 'Calculate' Button then call this method

        ValidateScore(userInput_score, activityType); //Populate and change userInput_score with textBox' Text and activity type to name/var
    }

    public static string ActivityType(string textboxName)
    {
        string trimmedName = textboxName.ToLower();
        if (trimmedName != "examitems" || trimmedName != "examscores") trimmedName = trimmedName.Substring(0, 4);

        switch (trimmedName)
        {
            case "quiz":
                return "quiz";
            case "laba":
                return "labAct";
            case "class":
                return "classAct";
            case "exam":
                return "exam";
            default:
                throw new ArgumentException($"Invalid textbox name: {textboxName}");
        }
    }

    // TOTAL ITEMS VALIDATION
    public static void ValidateTotalItems(string stringInput, string activityType)
    {
        int intTotalItems = 0;
        if (!int.TryParse(stringInput, out intTotalItems))
        {
            throw new ArgumentException($"Invalid input for {activityType}. Please enter a valid integer value.");
        }

        switch (ActivityType(activityType))
        {
            case "quiz":
                ValidateRange(intTotalItems, 5, 50);
                break;
            case "labAct":
                ValidateRange(intTotalItems, 50, 100);
                break;
            case "classAct":
                ValidateRange(intTotalItems, 30, 100);
                break;
            case "exam":
                ValidateRange(intTotalItems, 100, 100);
                break;
            default:
                throw new ArgumentException($"Invalid activity type for {activityType}");
        }
        Console.WriteLine("TEST 1: PASSED"); //PLEASE REMOVE
        Console.WriteLine($"totalItems: {intTotalItems}\nActivity: {ActivityType(activityType)}\n"); //PLEASE REMOVE
    }

    public static void ValidateRange(int intTotalItems, int min, int max)
    {
        if (intTotalItems < min || intTotalItems > max)
        {
            throw new ArgumentOutOfRangeException($"The total items must be between {min} and {max}.");
        }
    }

    // SCORES VALIDATION
    public static void ValidateScore(string stringScore, string activityType)
    {
        int intScore = 0;
        if (string.IsNullOrWhiteSpace(stringScore))
        {
            intScore = 0;
        }
        else if (!int.TryParse(stringScore, out intScore))
        {
            throw new ArgumentException("Invalid input. Score must be an integer.");
        }
        if (intScore < 0 || intScore > ValidateRange(intScore, activityType))
        {
            throw new ArgumentOutOfRangeException($"The Score cannot be less than zero (0) and cannot exceed total items.");
        }
        Console.WriteLine("TEST 2: PASSED"); //PLEASE REMOVE
        Console.WriteLine($"Scores: {intScore}\nActivity: {ActivityType(activityType)}\n"); //PLEASE REMOVE
    }

    public static int ValidateRange(int score, string activityType)
    {
        string activity = ActivityType(activityType);
        char count = (activity == "exam") ? '\0' : activityType[^1];

        string textBox_Items = $"{activity}Items{count}";
        string textBoxText = ScoreValidator.userInput_items;  // Change to 'string textBoxText =  (textBox_Items).Text;' to get The total Items Texbox Text
        if (int.TryParse(textBoxText, out int totalItems))
        {
            return totalItems;
        }
        return 0;
    }
}
