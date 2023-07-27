#pragma warning disable

public class FightForm
{
    private Form form;
    public FightForm(int ticks = 15)
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

        Ruyviu figther1 = new Ruyviu(new PointF(400, 1000), 1000);
        all_entities.Add(figther1);

        Ruyviu fighter2 = new Ruyviu(new PointF(800, 1000), 1000);
        all_entities.Add(fighter2);

        figther1.Enemy = fighter2;
        fighter2.Enemy = figther1;
        
        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer() {
            Interval = ticks
        };

        Player1 p1 = new Player1(figther1, "falas");
        Player2 p2 = new Player2(fighter2, "falas");


        Control.GetInstance(form);
        
        timer.Tick += delegate {
            g.Clear(Color.White); // clear screen

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
        };


    }

    public void Run()
    {
        Application.Run(form);
    }
}