# ProjectBattleArena
b.a

#구동 순서

interserver -> gamewebserver -> client

# inter server

- 웹 서버 관리 및 암호화에 필요한 키 생성

- command 

  ba gws on - 게임 웹 서버 on
  
  ba gws off - 게임 웹 서버 off
  
  ba close - inter server 종료

# interserverconfig.json

{
  "GameDB": {
    "EndPoint": "",
    "Port": 0,
    "UserId": "",
    "Password": "",
    "Database": ""
  },
  "GWSEndPoint": [
    "http://localhost:10000/KeepAlive"
  ]
}

# kosherLog.config

<?xml version="1.0" encoding="utf-8" ?>
<configuration>
	<targets>
		<target key ="consoleConfig" type ="console" format="test ${date} | ${level} | ${message}">
			<option type="color" key="Debug" value="DarkGray"/>
			<option type="color" key="Info" value="DarkGray"/>
			<option type="color" key="Error" value="DarkRed"/> 
			<option type="color" key="Fatal" value="DarkRed"/>
       	</target>
        <target key="fileConfig" type ="file" format="${date} | ${level} | ${message}">
			<option type ="fileName" value="./logs/LogFile.txt"/>
			<option type ="archiveFileName" value ="./archive/log.{#}.txt"/>
			<option type ="archiveRolling" value ="Day"/>
			<option type ="maxArchiveFiles" value ="7"/>
			<option type ="keepConnectionOpen" value="true"/>
        </target>
		<target key="databaseConfig" type="database">
			<database dbConnection ="use connection"
					dbProvider="MySql.Data.MySqlClient.MySqlConnection, MySql.Data"
				    dbCommandText="insert into log (logged, level, message, callsite)
										values (@logged, @level, @message, @callsite);">
				<option type="parameter" key="@level" value="${level}"></option>
				<option type="parameter" key="@logged" value="${date}"></option>
				<option type="parameter" key="@message" value="${message}"></option>
				<option type="parameter" key="@callsite" value="${callerFileName} : ${callerLineNumber}"></option>
			</database>
			<option type ="keepConnectionOpen" value="true"/>
		</target>
	</targets>
	<loggers>
		<logger key="consoleConfig" minLogLevel="Debug"></logger>
        <logger key ="fileConfig" name="" minLogLevel="Info"></logger>
		<logger key ="databaseConfig" name="" minLogLevel="Info"></logger>
	</loggers>
</configuration>

# game web server

- api 서버

# config.config

{
  "GameDB": {
    "EndPoint": "",
    "Port": 0,
    "UserId": "",
    "Password": "",
    "Database": ""
  }
}

# client

- MVC 패턴 구현

  1.SceneController - 컨트롤러
  
  - BindScene 을 통하여 Scene을 연결
  
  - 컨트롤러는 뷰 모델에 데이터 할당 및 씬에게 UI 갱신 요청
  
  2.BaseScene<TModel> - 뷰(씬)
  
  제네릭 타입을 통해 뷰 모델을 연결
  
  3.BaseScene의 모델 - 뷰 모델  
