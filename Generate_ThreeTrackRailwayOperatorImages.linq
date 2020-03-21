<Query Kind="Program">
  <Namespace>static UserQuery.Rop</Namespace>
  <Namespace>System.Drawing</Namespace>
  <Namespace>System.Drawing.Drawing2D</Namespace>
</Query>

void Main()
{
	var cfg = new Config();

	var rops = RopOperators.ToList();
	rops.Select(rop => $"![{rop.name.Item1}](images/{rop.name.Item1.Replace("/", "_")}.png)").Dump();

	DrawMargin(cfg, true);
	foreach(var rop in rops)
		DrawRopDiagram(cfg, rop);
	DrawMargin(cfg, false);
}

public enum Rop { V,N,E }
public static T[] L<T>(params T[] array) => array;

public List<((string, string) name, Rop[] funcs, (Rop, Rop)[] edges, bool end, bool start)> RopOperators = new List<((string, string), Rop[], (Rop, Rop)[], bool, bool)>{
	(("MyFunc","A => Result<B>"), L(V), L((V,V),(V,N),(V,E)), false, true),
	(("Tap","A => void"), L(V), L((V,V),(N,N),(E,E)), false, false),
	(("Bind","A => Result<B>"), L(V), L((V,V),(N,N),(E,E),(V,N),(V,E)), false, false),
	(("Map","A => B"), L(V), L((V,V),(N,N),(E,E)), false, false),
	(("Map2","A => B?"), L(V), L((V,V),(N,N),(E,E),(V,N)), false, false), //NoneIf
	(("Try","A => B?+Exception"), L(V), L((V,V),(N,N),(E,E),(V,N),(V,E)), false, false),
	(("Ensure","A => bool, Error"), L(V), L((V,V),(E,E),(V,E),(N,E)), false, false),
	(("IfNoneDefault","() => A"), L(N), L((V,V),(N,V),(E,E)), false, false),
	(("IfNoneFail","() => Error"), L(N), L((V,V),(N,E),(E,E)), false, false),
	(("OnFailureNone",""), L(E), L((V,V),(N,N),(E,N)), false, false),
	(("OnFailure","Error => void"), L(E), L((V,V),(N,N),(E,E)), false, false),
	(("OnFailureCompensate","Error => Result<A>"), L(E), L((V,V),(N,N),(E,E),(E,N),(E,V)), false, false),
	(("Elvis/Coalesce","() => Result<A>"), L(N), L((V,V),(N,N),(E,E),(N,V),(N,E)), false, false), //Coalesce
	(("Match","A => Result<B>, () => Result<B>, Error => Result<B>"), L(V,N,E), L((V,V),(N,N),(E,E),(V,N),(V,E),(N,V),(N,E),(E,N),(E,V)), false, false),
	(("Match2","A => B, () => B, Error => B"), L(V,N,E), L((V,V),(N,V),(E,V)), true, false)
};

public void DrawRopDiagram(Config cfg, ((string,string) name, Rop[] funcs, (Rop, Rop)[] edges, bool end, bool start) rop)
{
	var w = 5; var h = 6;
	var dx = 40; var dy = 30;
	using (var bitmap = new Bitmap(w * dx, h * dy))
	using (var g = Graphics.FromImage(bitmap))
	{
		g.Clear(cfg.BgColor);
		var root = new Box(g, 0, 0, w, h, dx, dy);
		root.DrawGrid(cfg.GridPen);

		var ropOpBox = root.With(0, 0, 5, 6);
		DrawRopOperator(cfg, ropOpBox, rop);

		bitmap.Dump("rop diagram");
		var fileName = Path.Combine(BaseFolder, $"{rop.name.Item1.Replace("/", "_")}.png");
		bitmap.Save(fileName);
	}
}
public static string BaseFolder = @"D:\Workspace\CSharp.Rop.Generic.Result\images";

public void DrawMargin(Config cfg, bool start)
{
	var w = 1; var h = 6;
	var dx = 40; var dy = 30;
	using (var bitmap = new Bitmap(w * dx, h * dy))
	using (var g = Graphics.FromImage(bitmap))
	{
		g.Clear(cfg.BgColor);
		var root = new Box(g, 0, 0, w, h, dx, dy);
		root.DrawGrid(cfg.GridPen);
		if(start)
		{
			var inputMarkerBox = root.With(0, 1, 1, 4);
			inputMarkerBox.DrawString("V", cfg.BigFont, Brushes.Green, 0.5f, 1)
				.DrawString("N", cfg.BigFont, Brushes.Blue, 0.5f, 2)
				.DrawString("E", cfg.BigFont, Brushes.Red, 0.5f, 3);
		}
		var name = $"margin_{(start ? "start" : "end")}";
		bitmap.Dump(name);
		var fileName = Path.Combine(BaseFolder, $"{name}.png");
		bitmap.Save(fileName);
	}
}

public void DrawRopOperator(Config cfg, Box ropOpBox, ((string,string) name, Rop[] funcs, (Rop, Rop)[] edges, bool end, bool start) rop)
{
	var innerBox = ropOpBox.With(0, 1, 5, 4);
	innerBox.HLine(cfg.GreenBoldPen, 0, 1, 2);
	if (!rop.start)
		innerBox.HLine(cfg.BlueBoldPen, 0, 2, 2).HLine(cfg.RedBoldPen, 0, 3, 2);
	innerBox.HLine(cfg.GreenBoldPen, 3, 1, 2);
	if (!rop.end)
		innerBox.HLine(cfg.BlueBoldPen, 3, 2, 2).HLine(cfg.RedBoldPen, 3, 3, 2);

	var junctionBox = innerBox.With(2, 1, 1, 2);
	foreach (var edge in rop.edges)
		junctionBox.DrawJunction(cfg.GetPen((int)edge.Item2), (int)edge.Item1, (int)edge.Item2);

	for (var ix = 0; ix < 2; ix++)
		for (var iy = 0; iy < 3; iy++)
			junctionBox.DrawPoint(cfg.BlackBoldPen, ix, iy);

	foreach (var func in rop.funcs)
		junctionBox.DrawFunc(cfg.FuncPen, cfg.BgBrush, (int)func);

	var titleBox = ropOpBox.With(0, 0, 5, 1);
	titleBox.DrawString(rop.name.Item1, cfg.BigFont, Brushes.Black, 1, 0.5f);
	if(!rop.start)
		innerBox.DrawRectangle(cfg.ContainerPen, 1, 0, 3, 4);
}

public struct Box
{
	private readonly Graphics _g;
	private readonly float _x;
	private readonly float _y;
	private readonly float _w;
	private readonly float _h;
	private readonly int _dx;
	private readonly int _dy;
	
	public Box(Graphics g, float x, float y, float w, float h, int dx, int dy)
	{
		_g = g;
		(_x, _y, _w, _h, _dx, _dy) = (x, y, w, h, dx, dy);
	}
	
	public Box WithOffset(float ox, float oy)
		=> new Box(_g, _x, _y, _w, _h, _dx, _dy);
		
	public Box WithSize(float w, float h)
		=> new Box(_g, _x, _y, w, h, _dx, _dy);
		
	public Box With(float ox, float oy, float w, float h)
		=> new Box(_g, _x + ox, _y + oy, w, h, _dx, _dy);

	public Box HLine(Pen pen, float nx, float ny, float l)
	{
		_g.DrawLine(pen, (_x + nx) * _dx, (_y + ny) * _dy, (_x + nx + l) * _dx, (_y + ny) * _dy);
		return this;
	}

	public Box DrawString(string s, Font font, Brush brush,  float nx, float ny)
	{
		_g.DrawString( s, font, brush, (_x + nx) * _dx - 5, (_y + ny) * _dy - 12);
		return this;
	}

	public Box DrawJunction(Pen pen, int start, int end)
	{
		_g.DrawLine(pen, _x * _dx, (_y + start) * _dy, (_x + 1) *_dx, (_y + end) * _dy);
		return this;
	}

	public Box DrawPoint(Pen pen, float nx, float ny)
	{
		var d = 2;
		_g.DrawEllipse(pen, (_x + nx) * _dx - d, (_y + ny) * _dy - d, 2 * d, 2 * d);
		return this;
	}

	public Box DrawFunc(Pen pen, Brush brush, int p)
	{
		var d = 10;
		_g.FillEllipse(brush, _x * _dx - d, (_y + p) * _dy - d, 2 * d, 2 * d);
		_g.DrawEllipse(pen, _x * _dx - d, (_y + p) * _dy - d, 2 * d, 2 * d);
		return this;
	}

	public Box DrawRectangle(Pen pen, float nx, float ny, float nw, float nh)
	{
		_g.DrawRectangle(pen, (_x + nx) * _dx, (_y + ny) * _dy, nw * _dx, nh * _dy);
		return this;
	}

	public void DrawGrid(Pen pen)
	{
		for (var i = 0; i <= _h; i++)
			_g.DrawLine(pen, 0, i * _dy, _w * _dx, i * _dy);
		for (var j = 0; j <= _w; j++)
			_g.DrawLine(pen, j * _dx, 0, j * _dx, _h * _dy);
	}

}

public class Config
{
	private static Color _bgColor = Color.LightGray;
	private static Color _gridColor = Color.DarkGray;

	public Color BgColor = _bgColor;
	public Color GridColor = _gridColor;

	public Pen FuncPen = new Pen(Color.DarkOrange, 5);
	public Pen ContainerPen = new Pen(Color.Black, 2);
	public Pen GreenBoldPen = new Pen(Color.Green, 5);
	public Pen BlueBoldPen = new Pen(Color.Blue, 5);
	public Pen RedBoldPen = new Pen(Color.Red, 5);
	public Pen BgBoldPen = new Pen(_bgColor, 5);
	public Pen BlackBoldPen = new Pen(Color.Black, 5);

	public Pen GridPen = new Pen(_gridColor);

	public Brush GreenBrush = new SolidBrush(Color.Green);
	public Brush BlueBrush = new SolidBrush(Color.Blue);
	public Brush RedBrush = new SolidBrush(Color.Red);
	public Brush BgBrush = new SolidBrush(_bgColor);

	public Font BigFont = new Font("Arial", 16, FontStyle.Bold);
	public Font SmallFont = new Font("Arial", 10, FontStyle.Bold);

	public Pen GetPen(int pos) 
		=> pos == 0 ? GreenBoldPen
		: pos == 1 ? BlueBoldPen
		: pos == 2 ? RedBoldPen
		: throw new InvalidOperationException();
		
}

public static String HexConverter(System.Drawing.Color c)
{
	return "#" + c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");
}

public static String RGBConverter(System.Drawing.Color c)
{
	return "RGB(" + c.R.ToString() + "," + c.G.ToString() + "," + c.B.ToString() + ")";
}