SET EMAIL_FROM=%~1
SET EMAIL_SUBJECT=%~2
SET EMAIL_TEXT=%~3
SET SENDGRID_URL=https://api.sendgrid.com/api/mail.send.json
SET API_USER=azure_38d70ae842d05afac3185942d4d6b0f6@azure.com
SET API_KEY=jHPBdEzn72zF

:: DOS way to set a variable from cmd output
FOR /F "tokens=* USEBACKQ" %%F IN (`git log -1 --pretty^=format:"%%ae"`) DO (
    SET EMAIL_TO=%%F
)

curl -k -X POST "%SENDGRID_URL%" --data-urlencode "to=%EMAIL_TO%" --data-urlencode "from=%EMAIL_FROM%"  --data-urlencode "subject=%EMAIL_SUBJECT%"  --data-urlencode "text=%EMAIL_TEXT%"  --data-urlencode "api_user=%API_USER%"  --data-urlencode "api_key=%API_KEY%"
