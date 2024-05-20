namespace maturita_rpg
{
    partial class Game
    {
        public List<string>? ActionText; // private
        public int ActionTextBoxWidth; // private
        public int actionLineIndex; // private

        //prints action text into the right segment
        public void PrintActionText() //private
        {
            for (int i = actionLineIndex; i < ActionText.Count && i <= mapBoxHeight; i++)
            {
                Console.SetCursorPosition(mapOffsetLeft + mapBoxWidth + 2, mapOffsetTop + i);
                Console.Write(ActionText[i]);
                actionLineIndex++;
            }
        }

        //used to log player's actions
        public void WriteIntoActionText(string text)
        {          
            //determines the number of lines needed to wright the text
            int textLineCount = text.Length / ActionTextBoxWidth;
            if (text.Length % ActionTextBoxWidth != 0)
            {
                textLineCount++;
            }

            if (actionLineIndex + textLineCount > mapBoxHeight) //clear action log if the text doesn't fit
            {
                ActionText.Clear();
                EraseActionText();
            }

            if (textLineCount == 1)
            {
                ActionText.Add(text);
            }

            //separates text into lines and adds them to the action log
            else
            {
                int overlapTextLength = text.Length % ActionTextBoxWidth;                

                int tmpInt = 0;
                while (tmpInt < textLineCount - 1 || (tmpInt < textLineCount && overlapTextLength == 0)) //for the lines that cover the full width of the box
                {
                    string tmp = "";
                    for (int i = tmpInt * ActionTextBoxWidth; i < (tmpInt + 1) * ActionTextBoxWidth; i++)
                    {
                        tmp = tmp + text[i];
                    }
                    ActionText.Add(tmp);

                    tmpInt++;
                }

                //overlap
                string tmp2 = "";
                for (int i = (textLineCount - 1) * ActionTextBoxWidth; i < (textLineCount - 1) * ActionTextBoxWidth + overlapTextLength; i++)
                {
                    tmp2 = tmp2 + text[i];
                }
                ActionText.Add(tmp2);
            }
            //actual print
            PrintActionText();
        }

        public void EraseActionText() //private
        {
            for (int y = 0; y < mapBoxHeight; y++)
            {
                Console.SetCursorPosition(mapOffsetLeft + mapBoxWidth + 2, mapOffsetTop + y);
                for (int x = 0; x < mapBoxWidth; x++)
                {
                    Console.Write(" ");
                }
            }
            actionLineIndex = 0;
        }

        

    }
}
