package bot;

import java.awt.Point;

public class Missile {
	public String Location;
	public int ExplosionRadius;
	public MoveDirection MoveDirection;

	public Point LocationToPoint() {
		String[] coords = Location.split(",");
		return new Point(Integer.parseInt(coords[0].trim()),
				Integer.parseInt(coords[1].trim()));
	}
}
