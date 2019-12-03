using System;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Java.Lang;
using String = System.String;

namespace tictactoe4
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private Button[][] buttons = { new Button[3], new Button[3], new Button[3] };
        private Button buttonReset;

        private bool player1turn = true;

        private int roundCount;

        private int player1Points;
        private int player2Points;

        private TextView textViewPlayer1;
        private TextView textViewPlayer2;


        protected internal override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView = Resource.Layout.activity_main;

            textViewPlayer1 = FindViewById(Resource.Id.text_view_p1);
            textViewPlayer2 = FindViewById(Resource.Id.text_view_p2);

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    string buttonID = "button_" + i + j;
                    int resID = Resources.GetIdentifier(buttonID, "id", PackageName);
                    buttons[i][j] = FindViewById(resID);
                    buttons[i][j].OnClickListener = this;
                }
            }

            buttonReset = FindViewById(R.id.button_reset);
            buttonReset.SetOnClickListener(new OnClickListenerAnonymousInnerClass(this));
        }

        private class OnClickListenerAnonymousInnerClass : View.IOnClickListener
        {
            private readonly MainActivity outerInstance;

            public OnClickListenerAnonymousInnerClass(MainActivity outerInstance)
            {
                this.outerInstance = outerInstance;
            }

            public IntPtr Handle => throw new NotImplementedException();

            public void Dispose()
            {
                throw new NotImplementedException();
            }

            public override void onClick(View v)
            {
                outerInstance.resetGame();

            }

            public void OnClick(View v)
            {
                throw new NotImplementedException();
            }
        }

        public override void OnClick(View v)
        {
            if (!((Button)v).Text.ToString().Equals(""))
            {
                return;
            }

            if (player1turn)
            {
                ((Button)v).Text = "x";
                v.SetBackgroundResource = Resource.Drawable.triangle;
            }
            else
            {
                ((Button)v).Text = "o";
                v.SetBackgroundResource = Resource.Drawable.square;
            }

            roundCount++;

            if (checkForWin())
            {
                if (player1turn)
                {
                    player1Wins();
                }
                else
                {
                    player2Wins();
                }
            }
            else if (roundCount == 9)
            {
                draw();
            }
            else
            {
                player1turn = !player1turn;
            }

        }

        private bool checkForWin()
        {
            string[][] field = { new string[3], new string[3], new string[3] };

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    field[i][j] = buttons[i][j].Text.ToString();
                }
            }

            for (int i = 0; i < 3; i++)
            {
                if (field[i][0].Equals(field[i][1]) && field[i][0].Equals(field[i][2]) && !field[i][0].Equals(""))
                {
                    return true;
                }
            }

            for (int i = 0; i < 3; i++)
            {
                if (field[0][i].Equals(field[1][i]) && field[0][i].Equals(field[2][i]) && !field[0][i].Equals(""))
                {
                    return true;
                }
            }

            if (field[0][0].Equals(field[1][1]) && field[0][0].Equals(field[2][2]) && !field[0][0].Equals(""))
            {
                return true;
            }

            if (field[0][2].Equals(field[1][1]) && field[0][2].Equals(field[2][0]) && !field[0][2].Equals(""))
            {
                return true;
            }

            return false;
        }

        private void player1Wins()
        {
            player1Points++;
            Toast.MakeText(this, "Player 1 wins!", Toast.LENGTH_SHORT).show();
            updatePointsText();
            resetBoard();
        }

        private void player2Wins()
        {
            player2Points++;
            Toast.MakeText(this, "Player 1 wins!", Toast.LENGTH_SHORT).show();
            updatePointsText();
            resetBoard();
        }

        private void draw()
        {
            Toast.MakeText(this, "Draw!", Toast.LENGTH_SHORT).show();
            resetBoard();
        }

        private void updatePointsText()
        {
            textViewPlayer1.Text = "Player 1: " + player1Points;
            textViewPlayer2.Text = "Player 2: " + player2Points;
        }

        private void resetBoard()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    buttons[i][j].Text = "";
                    buttons[i][j].Background = buttonReset.Background;
                }
            }

            roundCount = 0;
            player1turn = true;
        }

        private void resetGame()
        {
            player1Points = 0;
            player2Points = 0;
            updatePointsText();
            resetBoard();
        }

        protected internal override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);

            outState.PutInt("roundCount", roundCount);
            outState.PutInt("player1Points", player1Points);
            outState.PutInt("player2Points", player2Points);
            outState.PutBoolean("player1turn", player1turn);
        }

        //JAVA TO C# CONVERTER TODO TASK: Most Java annotatio
        protected internal override void OnRestoreInstanceState(Bundle savedInstanceState)
        {
            base.OnRestoreInstanceState(savedInstanceState);

            roundCount = savedInstanceState.GetInt("roundCount");
            player1Points = savedInstanceState.GetInt("player1Points");
            player2Points = savedInstanceState.GetInt("player2Points");
            player1turn = savedInstanceState.GetBoolean("player1turn");
        }


    }
}

