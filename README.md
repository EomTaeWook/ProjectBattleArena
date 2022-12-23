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

https://github.com/EomTaeWook/Kosher/blob/main/Kosher.Extensions/KosherLog.config

# game web server

- api 서버

# config.json

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
