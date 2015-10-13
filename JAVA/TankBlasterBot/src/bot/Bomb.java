package bot;

import java.awt.Point;

public class Bomb {
	public String Location;
	public int RoundsUntilExplodes;
	public int ExplosionRadius;

	public Point LocationToPoint() {
		String[] coords = Location.split(",");
		return new Point(Integer.parseInt(coords[0].trim()),
				Integer.parseInt(coords[1].trim()));
	}
}