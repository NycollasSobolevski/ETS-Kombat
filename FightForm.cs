#pragma warning disable

public class FightForm
{
    private Form form;
    public FightForm(int ticks = 12)
    {
        form = new Form();

        form.WindowState = FormWindowState.Maximized;
        form.FormBorderStyle = FormBorderStyle.None;
        form.Dock = DockStyle.Fill;

        PictureBox pb = new PictureBox();
        Graphics g = null;
        Bitmap bmp = null;

        pb.Dock = DockStyle.Fill;
        form.Controls.Add(pb);

        List<Entity> all_entities = new List<Entity>();
        //! Entities
        FpsCounter fps = new FpsCounter();
        all_entities.Add(fps);
        fps.Ticks = ticks;

        Background bg = new Background("Assets/Background/RandomParking.jpg", new SizeF(0, 0));

        Ruyviu figther1 = new Ruyviu(new PointF(400, 2000), 1000);
        all_entities.Add(figther1);

        Kaqui fighter2 = new Kaqui(new PointF(800, 2000), 1000);
        all_entities.Add(fighter2);

        figther1.Enemy = fighter2;
        fighter2.Enemy = figther1;
        
        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer() {
            Interval = ticks
        };

        Player1 p1 = new Player1(figther1, "falas");
        Player2 p2 = new Player2(fighter2, "nico");


        Control.GetInstance(form);
        
        timer.Tick += delegate {
            g.Clear(Color.White);
            // bg.Draw(g);

            foreach (var item in all_entities)
                item.Update(g, DateTime.Now);

            foreach (var item in all_entities)
                item.Draw(g);
            
            figther1.ChangeState(
                Control.GetState(p1, p1.Fighter)
            );
            
            fighter2.ChangeState(
                Control.GetState(p2, p2.Fighter)
            );

            pb.Refresh();
        };

        form.Load += delegate {
            timer.Start();

            bmp = new Bitmap(pb.Width, pb.Height);
            g = Graphics.FromImage(bmp);

            figther1.ScreenSize = new Size(bmp.Width, bmp.Height);
            fighter2.ScreenSize = figther1.ScreenSize;

            figther1.HealthPoints = new Life(figther1.ScreenSize, 1);
            fighter2.HealthPoints = new Life(figther1.ScreenSize, 0);
            
            pb.Image = bmp;

            bg = new Background("Assets/Background/RandomParking.jpg", figther1.ScreenSize);
        };


    }

    public void Run()
    {
        Application.Run(form);
    }
}