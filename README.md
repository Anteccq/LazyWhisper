#  Lazy Whisper [1.0.1]
>Lazy Whisper is a simply custom command bot for Discord.

It needs a appsettings.{Environment}.json file.

```
// appsettings.{Environment}.json
"Config": {
    "Token": "Discord token",
    "ConnectionStrings": "{Database connection strings (mysql, mariadb)}"
}
```

## Commands 
In default, Lazy Whisper has 5 commands.

!add, !edit, !remove, !list, !help

### Adding
```
!add Command Message
```
### Editing
```
!edit Command Message
```
### Deleting
```
!remove Command
```
### Command List
```
!list
```
### Information (Default command list)
```
!help
```

## Development
### Requirments
* .NET Core 3.1
* MariaDB 10.5

## License
Under the MIT.