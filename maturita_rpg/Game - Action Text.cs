using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maturita_rpg
{
    internal partial class Game
    {
        public List<string>? ActionText;
        public int ActionTextBoxWidth;
        int actionLineIndex;

        public void PrintActionText()
        {
            for (int i = actionLineIndex; i < ActionText.Count && i <= mapBoxHeight; i++)
            {
                Console.SetCursorPosition(mapOffsetLeft + mapBoxWidth + 2, mapOffsetTop + i);
                Console.Write(ActionText[i]);
                actionLineIndex++;
            }
        }

        public void WriteIntoActionText(string text)
        {
            { /*int textLineCount = 1;
            if (actionLineIndex + textLineCount > mapBoxHeight)
            {
                ActionText.Clear();
                EraseActionText();
            }
            if (text.Length <= ActionTextBoxWidth)
            {
                ActionText.Add(text);
            }
            else
            {

                textLineCount = 2;
                if (actionLineIndex + textLineCount > mapBoxHeight)
                {
                    ActionText.Clear();
                    EraseActionText();
                }
                string tmp = "";
                for (int i = 0; i < ActionTextBoxWidth; i++)
                {
                    tmp = tmp + text[i];
                }
                ActionText.Add(tmp);

                tmp = "";
                for (int i = ActionTextBoxWidth ; i < 2 * ActionTextBoxWidth && i < text.Length; i++)
                {
                    tmp = tmp + text[i];
                }
                ActionText.Add(tmp);

                if (text.Length > ActionTextBoxWidth * 2)
                {
                    textLineCount = 3;
                    if (actionLineIndex + textLineCount > mapBoxHeight)
                    {
                        ActionText.Clear();
                        EraseActionText();
                    }
                    tmp = "";
                    for (int i = 2 * ActionTextBoxWidth; i < 3 * ActionTextBoxWidth && i < text.Length; i++)
                    {
                        tmp = tmp + text[i];
                    }
                    ActionText.Add(tmp);
                }
            }*/
            } //old 

            int textLineCount = text.Length / ActionTextBoxWidth;
            if (text.Length % ActionTextBoxWidth != 0)
            {
                textLineCount++;
            }

            if (actionLineIndex + textLineCount > mapBoxHeight)
            {
                ActionText.Clear();
                EraseActionText();
            }

            if (textLineCount == 1)
            {
                ActionText.Add(text);
            }
            else
            {
                int overlapTextLength = text.Length % ActionTextBoxWidth;                

                int tmpInt = 0;
                while (tmpInt < textLineCount - 1 || (tmpInt < textLineCount && overlapTextLength == 0))
                {
                    string tmp = "";
                    for (int i = tmpInt * ActionTextBoxWidth; i < (tmpInt + 1) * ActionTextBoxWidth; i++)
                    {
                        tmp = tmp + text[i];
                    }
                    ActionText.Add(tmp);

                    tmpInt++;
                }

                string tmp2 = "";
                for (int i = (textLineCount - 1) * ActionTextBoxWidth; i < (textLineCount - 1) * ActionTextBoxWidth + overlapTextLength; i++)
                {
                    tmp2 = tmp2 + text[i];
                }
                ActionText.Add(tmp2);
            }

            PrintActionText();
        }

        public void EraseActionText()
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
