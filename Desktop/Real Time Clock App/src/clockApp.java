
import javax.swing.*;
import java.awt.*;
import java.time.ZoneId;
import java.time.ZonedDateTime;
import java.time.format.DateTimeFormatter;

        public class clockApp extends Canvas {

            public void paint(Graphics g) {
                // set the font and text alignment
                g.setFont(new Font("SansSerif", Font.PLAIN, 30));
                g.setColor(Color.BLACK);

                // using an API to grab the time of New York
                // same template used for other parts of the world
                ZonedDateTime time = ZonedDateTime.now(ZoneId.of("America/New_York"));
                String timeString = time.format(DateTimeFormatter.ofPattern("HH:mm"));

                //draws for New York
                g.drawRect(-10, 65, 520, 50);
                g.drawString("New York:", 10, 100);
                g.drawString(timeString, 400, 100);

                //API pull for LA in time2
                ZonedDateTime time2 = ZonedDateTime.now(ZoneId.of("America/Los_Angeles"));
                String timeString2 = time2.format(DateTimeFormatter.ofPattern("HH:mm"));

                //draws for LA
                g.drawRect(-10, 165, 520, 50);
                g.drawString("LA:", 10, 200);
                g.drawString(timeString2, 400, 200);

                //API pull for Paris in time2

                ZonedDateTime time3 = ZonedDateTime.now(ZoneId.of("Europe/Paris"));
                String timeString3 = time3.format(DateTimeFormatter.ofPattern("HH:mm"));

                //draws for Paris
                g.drawRect(-10, 265, 520, 50);
                g.drawString("Paris:", 10, 300);
                g.drawString(timeString3, 400, 300);

                //API pull for Tokyo Time
                ZonedDateTime time4 = ZonedDateTime.now(ZoneId.of("Asia/Tokyo"));
                String timeString4 = time4.format(DateTimeFormatter.ofPattern("HH:mm"));

                //draws for Tokyo
                g.drawRect(-10, 365, 520, 50);
                g.drawString("Tokyo:", 10, 400);
                g.drawString(timeString4, 400, 400);
            }

            public static void main(String[] args) {
                // create a window to display the canvas
                JFrame frame = new JFrame("Times Around The World");
                frame.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
                frame.setSize(500, 500);

                // add the canvas to the window
                clockApp clock = new clockApp();
                frame.add(clock);

                // show the window
                frame.setVisible(true);
            }
        }