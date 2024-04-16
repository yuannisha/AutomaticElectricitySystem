/* eslint-disable prettier/prettier */
import type { AppRouteModule } from '/@/router/types';
import { LAYOUT } from '/@/router/constant';
// import { t } from '/@/hooks/web/useI18n';
const tenant: AppRouteModule = {
  path: '/powerSwitchs',
  name: 'PowerSwitchs',
  component: LAYOUT,
  meta: {
    orderNo: 30,
    icon: 'ant-design:contacts-outlined',
    // title: t('教室预约'),
    title:"智能电表管理",
    policy:"PowerSwitchs",
  },
  children: [
    {
      path: 'powerSwitchs',
      name: 'PowerSwitchs',
      component: () => import('/@/views/powerSwitchs/index.vue'),
      meta: {
        // title: t('routes.tenant.tenantList'),
        title:"智能电表",
        icon: 'ant-design:switcher-filled',
        policy:"PowerSwitchs.PowerSwitchsManagement.view"
      },
    },
  ],
};

export default tenant;
