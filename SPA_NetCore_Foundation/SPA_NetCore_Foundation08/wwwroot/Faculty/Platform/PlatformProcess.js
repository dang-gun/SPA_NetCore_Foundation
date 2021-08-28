/** 플랫폼정보를 서버에서 사용하기위해 필요한 작업들을 한다.*/
var PlatformProcess = {};

/** 판단된 플랫폼 */
PlatformProcess.PlatformType = PlatformType.None;

/** GUID */
PlatformProcess.GUID = "";
/** GUID 용 쿠기 이름 */
PlatformProcess.GUID_CookieName = "GUID";



/** 판단된 플랫폼 */
PlatformProcess.Start = function ()
{
    //저장된 guid 읽기
    PlatformProcess.GUID = CA.Get(PlatformProcess.GUID_CookieName);
    if ("" === PlatformProcess.GUID)
    {//기존 guid가 없다.

        //guid를 생성한다.
        PlatformProcess.GUID = uuidv4();
        //생성된 guid를 쿠키에 저장된다.
        CA.Set(PlatformProcess.GUID_CookieName
            , PlatformProcess.GUID
            , CA.SaveType.Infinite);
    }

    //플랫폼 판단
    PlatformProcess.PlatformToType();
};

/** 플랫폼 정보를 읽어서 서버에 맞게 변경한다. */
PlatformProcess.PlatformToType = function ()
{
    //플랫폼 정보 저장
    var jsonPlatform = platform;

    switch (jsonPlatform.os.family)
    {
        case "Windows":
        case "Linux":
        case "Mac OS X":
        case "Mac":
        case "CentOS":
        case "Debian":
        case "Fedora":
        case "FreeBSD":
        case "OpenBSD":
        case "Ubuntu":
            PlatformProcess.PlatformType = PlatformType.PC;
            break;

        case "Android":
            PlatformProcess.PlatformType = PlatformType.Mobile;
            break;

    }

    //플랫폼을 찾지 못했다.
    if (PlatformType.None === PlatformProcess.PlatformType)
    {
        //아이팟 계열인지 확인한다.
        switch (jsonPlatform.prerelease)
        {
            case "iPad":
            case "iPhone":
            case "iPod":
                PlatformProcess.PlatformType = PlatformType.Mobile;
                break;

        }
    }



    if (PlatformType.None === PlatformProcess.PlatformType)
    {//마지막까지 못찾음
        PlatformProcess.PlatformType = PlatformType.NotKnow;
    }
    
};