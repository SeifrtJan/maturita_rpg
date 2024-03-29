namespace maturita_rpg
{
    partial class Game
    {
        public List<string> CombatText;
        public int combatLineIndex;
        public void Combat(Enemy enemy)
        {
            EraseCombatText();
            WriteIntoActionText("you entered a fight with " + enemy.name);
            enemy.PrintInfo();
            CombatTick(enemy);
            RefreshPrint();
            
        }

        private void CombatTick(Enemy enemy)
        {
            Character attacking = player;
            Character waiting = enemy;

            while (!player.IsDead() && !enemy.IsDead())
            {
                attacking.TakeTurn(waiting);                

                Console.ReadKey(true);

                Character tmp = attacking;
                attacking = waiting;
                waiting = tmp;
            }
        }

        private void PrintCombatText()
        {
            EraseMapBox();
            for (int i = combatLineIndex; i < CombatText.Count && i <= mapBoxHeight; i++)
            {
                Console.SetCursorPosition(mapOffsetLeft, mapOffsetTop + i);
                Console.Write(CombatText[i]);
                combatLineIndex++;
            }
        }

        public void WriteIntoCombatText(string text)
        {
            int textLineCount = text.Length / mapBoxWidth;
            if (text.Length % mapBoxWidth != 0)
            {
                textLineCount++;
            }

            if (combatLineIndex + textLineCount > mapBoxHeight)
            {
                CombatText.Clear();
                EraseMapBox();
            }

            if (textLineCount == 1)
            {
                CombatText.Add(text);
            }
            else
            {
                int overlapTextLength = text.Length % mapBoxWidth;

                int tmpInt = 0;
                while (tmpInt < textLineCount - 1 || (tmpInt < textLineCount && overlapTextLength == 0))
                {
                    string tmp = "";
                    for (int i = tmpInt * mapBoxWidth; i < (tmpInt + 1) * mapBoxWidth; i++)
                    {
                        tmp = tmp + text[i];
                    }
                    CombatText.Add(tmp);

                    tmpInt++;
                }

                string tmp2 = "";
                for (int i = (textLineCount - 1) * mapBoxWidth; i < (textLineCount - 1) * mapBoxWidth + overlapTextLength; i++)
                {
                    tmp2 = tmp2 + text[i];
                }
                CombatText.Add(tmp2);
            }

            PrintCombatText();
        }

        public void EraseCombatText()
        {
            CombatText.Clear();
            combatLineIndex = 0;
        }
    }
}
