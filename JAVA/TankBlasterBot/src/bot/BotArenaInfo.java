package bot;

import java.awt.Point;
import java.util.ArrayList;
import java.util.List;
import java.util.UUID;

import com.google.gson.annotations.SerializedName;

public class BotArenaInfo {
	public int RoundNumber;
	public int TurnNumber;
	public UUID BotId;
	public int[][] Board;
	private String BotLocation;
	public int MissileAvailableIn;
	private List<String> OpponentLocations;
	public List<Bomb> Bombs;
	public List<Missile> Missiles;
	@SerializedName("GameConfig")
	public TankBlasterConfig GameConfig;

	public Point GetBotLocation() {
		String[] coords = BotLocation.split(",");
		return new Point(Integer.parseInt(coords[0].trim()),
				Integer.parseInt(coords[1].trim()));
	}

	public List<Point> GetOpponentLocationList() {
		List<Point> result = new ArrayList<Point>();

		for (String point : OpponentLocations) {
			String[] coords = point.split(",");
			result.add(new Point(Integer.parseInt(coords[0].trim()), Integer
					.parseInt(coords[1].trim())));
		}

		return result;
	}
}
