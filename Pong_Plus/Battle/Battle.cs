using System;
using System.Collections.Generic;
using Needler.UI;
using Needler.Graphics;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
namespace Needler.BattleSystem
{
    public enum BattleState
    {
        AwaitEnemyAction = 0,
        AwaitPlayerAction = 1,
        TurnState = 2,
        PreparingTurn = 3,
        EnemyVictory = 4,
        AllyVictory = 5
    }
    public class Battle
    {
        public List<AllyCharacter> allyCharacters;
        public List<EnemyCharacter> enemyCharacters;// I'll sort it later, but for now all ally characters go first, then enemy goes last
        public List<Character> characterOrder;
        public Queue<BattleAction> actionOrder;
        public List<TurnActionObject> cTurn;
        public int currentActorIndex;

        public BattleState cstate {
            get;
            private set;
        }

        public MenuManager menuManager;

        public DialogBox dialog;

        public BattleRenderer bRenderer;

        public int turnCount = 0;

        public Battle(List<AllyCharacter> allyCharacters, List<EnemyCharacter> enemyCharacters)
        {
            this.allyCharacters = allyCharacters;
            this.enemyCharacters = enemyCharacters;
            this.currentActorIndex = 0;
            this.menuManager = new MenuManager(this);
            this.dialog = new DialogBox(20, 40, Needler.scrwd-40, 100);
            this.bRenderer = new BattleRenderer(this);
            cstate = BattleState.AwaitEnemyAction;
            Console.Out.WriteLine("Battle has been initialized.");
            Console.Out.WriteLine("Characters -------------");
            Console.Out.WriteLine("Party 1 -");
            foreach (AllyCharacter a in allyCharacters)
            {
                Console.Out.WriteLine(a.ToString());
            }
            Console.Out.WriteLine("Party 2 -");
            foreach (EnemyCharacter a in enemyCharacters)
            {
                Console.Out.WriteLine(a.ToString());
            }
            Console.Out.WriteLine("----------------");
        }

        /*
         * HOW THE BATTLE WORKS
         * Decide actions for all characters
         *  -> If it's a player-controlled character, open menus to let them decide what to do through MenuActions
         *  -> If otherwise, let CPU choose
         *  
         *  Once all actions have been decided, turn execution begins; battle runs through every character's action(s) and only starts the next
         *  when the previous one has finished executing (notify through finishAction)
         *  
         *  Rinse and repeat
         */
        public void decideTurnOrderInitial()
        {
            turnCount++;
            Console.Out.WriteLine("========= Turn " + turnCount.ToString() + " =========");
            currentActorIndex = 0;
            actionOrder = new Queue<BattleAction>();
            characterOrder = new List<Character>();
            foreach (Character x in allyCharacters) {
                characterOrder.Add(x);
            }
            foreach (Character x in enemyCharacters)
            {
                characterOrder.Add(x);
            }
            characterOrder.Sort(new speedEvaluate());
            bRenderer.calculatePositions();
            if (characterOrder[0] is AllyCharacter)
            {
                cstate = BattleState.AwaitPlayerAction;
                ((AllyCharacter)characterOrder[0]).ToggleReady();
                promptPlayerDecide();
            }
            else
            {
                cstate = BattleState.AwaitEnemyAction;
                promptCPUDecide();
            }
        }
        private void promptPlayerDecide()
        {
            menuManager.openMenu(new BattleMenuBar(new List<UIOption>()
            {
                new UIOption("Attack", false, new Sprite(Needler.menuIconTextures, 0, 0, 10, 10, Color.White),new Sprite(Needler.menuIconTextures, 0, 10, 10, 10, Color.White), new Bash()),
                new UIOption("Items", false, new Sprite(Needler.menuIconTextures, 10, 0, 10, 10, Color.White),new Sprite(Needler.menuIconTextures, 10, 10, 10, 10, Color.White), new HealAct()),
            }, menuManager));
        }
        private void promptCPUDecide()
        {
            setAction(new BashAction(getCurrentCharActor(), new List<Character>(allyCharacters)));
        }
        public DamageText damage(Character target, int amount)
        {
            if (target is AllyCharacter)
            {
                ((AllyCharacter)target).potentialHP -= amount;
            }
            else if (target is EnemyCharacter) 
            {
                ((EnemyCharacter)target).currentHP -= amount;
            }
            return bRenderer.showDamage(amount, target, Color.White); ;
        }
        public DamageText heal(Character target, int amount)
        {
            if (target is AllyCharacter)
            {
                ((AllyCharacter)target).potentialHP += amount;
            }
            else if (target is EnemyCharacter)
            {
                ((EnemyCharacter)target).currentHP += amount;
            }
            return bRenderer.showDamage(amount, target, Color.LimeGreen);
        }
        public Character getCurrentCharActor()
        {
            return characterOrder[currentActorIndex];
        }
        public void setAction(BattleAction ba)
        {
            AllyCharacter current;
            Console.Out.WriteLine("Action enqueued for Character <" + getCurrentCharActor().ToString() + "> (" + (currentActorIndex+1).ToString() + "/" + characterOrder.Count.ToString() + ")");
            if (getCurrentCharActor() is AllyCharacter)
            {
                current = (AllyCharacter)getCurrentCharActor();
                current.ToggleReady();
            }
            actionOrder.Enqueue(ba);
            currentActorIndex++;
            if (currentActorIndex >= characterOrder.Count)
            {
                // prepare Combat
                cstate = BattleState.PreparingTurn;
                Console.Out.WriteLine("Starting battle execution...");
                return;
            }
            if (getCurrentCharActor() is AllyCharacter)
            {
                cstate = BattleState.AwaitPlayerAction;
                ((AllyCharacter)getCurrentCharActor()).ToggleReady();
                promptPlayerDecide();
            }
            else
            {
                cstate = BattleState.AwaitEnemyAction;
                promptCPUDecide();
            }
                
        }
        public void undoLastAction()
        {
            var chr = getCurrentCharActor();
            if (currentActorIndex == 0)
            {
                return;
            }
            if (chr is AllyCharacter) { ((AllyCharacter)chr).ToggleReady(); }
            menuManager.closeAllMenus();
            actionOrder.Dequeue();
            currentActorIndex--;
            chr = getCurrentCharActor();
            if (chr is AllyCharacter) {
                ((AllyCharacter)chr).ToggleReady();
                cstate = BattleState.AwaitPlayerAction;
                promptPlayerDecide();
            }
            else { undoLastAction(); }
        }
        public void Update(GameTime gametime)
        {
            
            if (cstate == BattleState.TurnState)
            {
                if (cTurn[0].Update(gametime))
                {
                    cTurn.RemoveAt(0);
                    /*
                    for (int i = 0; i < allyCharacters.Count; i++)
                    {
                        if (allyCharacters[i].status == cStatus.Dead)
                        {
                            
                            allyCharacters.RemoveAt(i); // Just kills them literally lmao
                            i--;
                        }
                    }
                    for (int i = 0; i < enemyCharacters.Count; i++)
                    {
                        if (enemyCharacters[i].status == cStatus.Dead)
                        {
                            enemyCharacters.RemoveAt(i); // Just kills them literally lmao
                            i--;
                        }
                    }
                    if (allyCharacters.Count == 0)
                    {
                        cstate = BattleState.EnemyVictory;
                        Console.Out.WriteLine("Enemies win!");
                        return;
                    }
                    else if (enemyCharacters.Count == 0)
                    {
                        cstate = BattleState.AllyVictory;
                        Console.Out.WriteLine("Allies win!");
                        return;
                    }
                    */
                    if (cTurn.Count <= 0)
                    { // Current turn is exhausted, nothing left to process
                        Console.Out.WriteLine("Turn finished.");

                        if (actionOrder.Count == 0) // All turns are finished, 
                        {
                            
                            decideTurnOrderInitial();
                        }
                        else // Some turns still left to cover, reset to Preparing Turn to automatically advance
                        {
                            cstate = BattleState.PreparingTurn;
                        }
                    }
                    else
                    {
                        cTurn[0].init();
                    }
                    // If it doesn't break, some things still left to do, just keep on going for the current turn (enumerator)
                }
            }
            else if (cstate == BattleState.PreparingTurn)
            {
                cTurn = actionOrder.Dequeue().battleExecute(this);
                if (cTurn.Count > 0)
                {
                    cTurn[0].init();
                    cstate = BattleState.TurnState;
                }
            }
        }
    }

}
