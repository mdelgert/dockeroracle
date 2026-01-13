
---

# 🟦 STEP 1 — Start the Oracle Free container

Run this exactly:

```bash
docker pull gvenzl/oracle-free:latest
docker run -d --name oracle-free -p 1521:1521 -p 5500:5500 -e ORACLE_PASSWORD=oracle gvenzl/oracle-free:latest
```

Check that it’s running:

```bash
docker ps
```

View logs until you see *DATABASE IS READY*:

```bash
docker logs -f oracle-free
```

---

# 🟦 STEP 2 — Enter the Oracle container

```bash
docker exec -it oracle-free bash
```

You are now inside the container.

---

# 🟦 STEP 3 — Connect to Oracle using SQL*Plus

Use SYSTEM:

```bash
sqlplus SYSTEM/oracle@localhost:1521/FREEPDB1
```

You should now see:

```
SQL>
```

---

# 🟦 STEP 4 — Create a dedicated user (schema)

### Create user:

```sql
CREATE USER weatheruser IDENTIFIED BY weatherpwd;
```

### Grant permissions:

```sql
GRANT CONNECT, RESOURCE TO weatheruser;
ALTER USER weatheruser QUOTA UNLIMITED ON USERS;
```

### Exit SYSTEM user:

```sql
EXIT;
```

---

# 🟦 STEP 5 — Log in as your new user

```bash
sqlplus weatheruser/weatherpwd@localhost:1521/FREEPDB1
```

---

# 🟦 STEP 6 — Create the **WeatherForecast** table

Based on your C# class:

```sql
CREATE TABLE WeatherForecast (
    Id NUMBER GENERATED ALWAYS AS IDENTITY,
    ForecastDate DATE,
    TemperatureC NUMBER,
    Summary VARCHAR2(200),
    PRIMARY KEY (Id)
);
```

### Notes:

* `Id` is added for SQL convenience (C# class doesn’t require it but databases do)
* `ForecastDate` holds your `DateOnly`
* `TemperatureF` is **not stored** (C# computes it)
* `Summary` is nullable like in your class

---

# 🟦 STEP 7 — Insert sample weather data

```sql
INSERT INTO WeatherForecast (ForecastDate, TemperatureC, Summary)
VALUES (DATE '2025-01-24', 5, 'Cold');

INSERT INTO WeatherForecast (ForecastDate, TemperatureC, Summary)
VALUES (DATE '2025-01-25', 12, 'Cool');

INSERT INTO WeatherForecast (ForecastDate, TemperatureC, Summary)
VALUES (DATE '2025-01-26', 22, 'Warm');

COMMIT;
```

You should see:

```
1 row created.
```

for each insert.

---

# 🟦 STEP 8 — Query the data

```sql
SELECT * FROM WeatherForecast;
```

You should see output like:

```
        ID FORECASTDATE  TEMPERATUREC SUMMARY
---------- ------------ ------------- -------------
         1 24-JAN-25               5 Cold
         2 25-JAN-25              12 Cool
         3 26-JAN-25              22 Warm
```

---

# 🟦 STEP 9 — Exit SQLPlus and the container

From SQLPlus:

```sql
EXIT;
```

From container shell:

```bash
exit
```

---

# 🟦 OPTIONAL (Recommended): Persist Data with Volume

So you don’t lose the DB when recreating the container:

```bash
docker run -d \
  --name oracle-free \
  -p 1521:1521 \
  -p 5500:5500 \
  -e ORACLE_PASSWORD=oracle \
  -v oracle-data:/opt/oracle/oradata \
  gvenzl/oracle-free:latest
```

---

# 🎉 Done!

## You now have:

✔ Running Oracle Free Docker instance
✔ Your own schema (`weatheruser`)
✔ A `WeatherForecast` table
✔ Real rows inserted
✔ Ability to query & manage data with **only Docker + SQLPlus**

## Universal UI
[dbeaver](https://dbeaver.io/)

---
