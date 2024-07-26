-- Create the table
CREATE TABLE WeatherSummaries (
    Id SERIAL PRIMARY KEY,
    Summary VARCHAR(50) NOT NULL
);

-- Insert the initial summaries
INSERT INTO WeatherSummaries (Summary) VALUES ('Freezing');
INSERT INTO WeatherSummaries (Summary) VALUES ('Bracing');
INSERT INTO WeatherSummaries (Summary) VALUES ('Chilly');
INSERT INTO WeatherSummaries (Summary) VALUES ('Cool');
INSERT INTO WeatherSummaries (Summary) VALUES ('Mild');
INSERT INTO WeatherSummaries (Summary) VALUES ('Warm');
INSERT INTO WeatherSummaries (Summary) VALUES ('Balmy');
INSERT INTO WeatherSummaries (Summary) VALUES ('Hot');
INSERT INTO WeatherSummaries (Summary) VALUES ('Sweltering');
INSERT INTO WeatherSummaries (Summary) VALUES ('Scorching');
