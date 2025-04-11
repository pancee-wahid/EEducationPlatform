import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

const oAuthConfig = {
  issuer: 'https://localhost:44331/',
  redirectUri: baseUrl,
  clientId: 'EEducationPlatform_App',
  responseType: 'code',
  scope: 'offline_access EEducationPlatform',
  requireHttps: true,
};

export const environment = {
  production: true,
  application: {
    baseUrl,
    name: 'EEducationPlatform',
  },
  oAuthConfig,
  apis: {
    default: {
      url: 'https://localhost:44331',
      rootNamespace: 'EEducationPlatform',
    },
    AbpAccountPublic: {
      url: oAuthConfig.issuer,
      rootNamespace: 'AbpAccountPublic',
    },
  },
  remoteEnv: {
    url: '/getEnvConfig',
    mergeStrategy: 'deepmerge'
  }
} as Environment;
