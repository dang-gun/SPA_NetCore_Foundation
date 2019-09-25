﻿/**
 * 사이트 전체에서 사용되는 정보
 */
var GlobalStatic = {};

/** 디버그 여부 */
GlobalStatic.Debug = false;

/** 
 *  사이트 타입
 * 0 : 일반적인 홈페이지(로그인없이 탐색 가능)
 * 1 : 어드민 타입(로그인없이 탐색 불가능)
 */
GlobalStatic.SiteType = 0;

/** 사인인 여부 */
GlobalStatic.SignIn = false;
/** 사인인 - 아이디 정보 */
GlobalStatic.SignIn_ID = "";
/** 사인인 - 토큰 정보 */
GlobalStatic.SignIn_token = "";

//현재 보고있는 페이지의 인스턴스
GlobalStatic.Page_Now = null;
//현재 보고있는 페이지의 구분용 타이틀
GlobalStatic.PageType_Now = "";