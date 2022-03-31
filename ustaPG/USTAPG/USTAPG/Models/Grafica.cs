using USTAPG.ViewModels;

namespace USTAPG.Models
{
    public class Grafica:BaseViewModel
    {
        public string x;
        public int y;

        public string X
        {
            get { return this.x; }
            set { SetValue(ref this.x, value); }
        }
        public int Y
        {
            get { return this.y; }
            set { SetValue(ref this.y, value); }
        }
    }
}
