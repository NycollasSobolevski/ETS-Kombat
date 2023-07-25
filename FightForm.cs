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

        Ruyviu k = new Ruyviu();
        // Joe k = new Joe();
        all_entities.Add(k);

        Joe j = new Joe();
        all_entities.Add(j);

        k.Enemy = j;
        j.Enemy = k;
        
        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer() {
            Interval = ticks
        };

        Control.GetInstance(form);
        
        timer.Tick += delegate {
            g.Clear(Color.White); // clear screen

            foreach (var item in all_entities)
                item.Update(g, fps.TimePerFrame);

            foreach (var item in all_entities)
                item.Draw(g);

            pb.Refresh();
        };

        form.Load += delegate {
            timer.Start();

            bmp = new Bitmap(pb.Width, pb.Height);
            g = Graphics.FromImage(bmp);

            k.ScreenSize = new Size(bmp.Width, bmp.Height);
            j.ScreenSize = k.ScreenSize;
            
            pb.Image = bmp;
        };
    }

    public void Run()
    {
        Application.Run(form);
    }
}