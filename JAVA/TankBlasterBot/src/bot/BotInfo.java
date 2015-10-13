package bot;

import javax.xml.bind.annotation.XmlRootElement;

@XmlRootElement(name = "BotInfo")
public class BotInfo {
	public String Name;
	public String Description;
	public String AvatarUrl;
	public String GameType;
}
