import {
  LogLevel,
  Configuration,
  BrowserCacheLocation,
} from '@azure/msal-browser';

const isIE =
  window.navigator.userAgent.indexOf('MSIE ') > -1 ||
  window.navigator.userAgent.indexOf('Trident/') > -1;

export const b2cPolicies = {
  names: {
    signUpSignIn: 'b2c_1_susi_reset_v2',
    editProfile: 'b2c_1_edit_profile_v2',
  },
  authorities: {
    signUpSignIn: {
      authority:
        'https://your-tenant-name.b2clogin.com/your-tenant-name.onmicrosoft.com/b2c_1_susi_reset_v2',
    },
    editProfile: {
      authority:
        'https://your-tenant-name.b2clogin.com/your-tenant-name.onmicrosoft.com/b2c_1_edit_profile_v2',
    },
  },
  authorityDomain: 'your-tenant-name.b2clogin.com',
};

export const msalConfig: Configuration = {
  auth: {
    clientId: '<your-MyApp-application-ID>',
    authority: b2cPolicies.authorities.signUpSignIn.authority,
    knownAuthorities: [b2cPolicies.authorityDomain],
    redirectUri: '/',
  },
  cache: {
    cacheLocation: BrowserCacheLocation.LocalStorage,
    storeAuthStateInCookie: isIE,
  },
  system: {
    loggerOptions: {
      loggerCallback: (logLevel, message, containsPii) => {
        console.log(message);
      },
      logLevel: LogLevel.Verbose,
      piiLoggingEnabled: false,
    },
  },
};

export const protectedResources = {
  todoListApi: {
    endpoint: 'http://localhost:5000/api/todolist',
    scopes: ['https://your-tenant-name.onmicrosoft.com/api/tasks.read'],
  },
};
export const loginRequest = {
  scopes: [],
};
