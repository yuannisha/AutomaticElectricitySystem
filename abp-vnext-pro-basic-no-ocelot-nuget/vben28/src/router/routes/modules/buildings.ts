import type { AppRouteModule } from '/@/router/types';
import { LAYOUT } from '/@/router/constant';
// import { t } from '/@/hooks/web/useI18n';
const tenant: AppRouteModule = {
  path: '/buildings',
  name: 'Buildings',
  component: LAYOUT,
  meta: {
    orderNo: 30,
    icon: 'ant-design:contacts-outlined',
    // title: t('教室预约'),
    title: '楼栋',
    policy: 'Buildings',
  },
  children: [
    {
      path: 'buildings',
      name: 'Buildings',
      component: () => import('/@/views/buildings/index.vue'),
      meta: {
        // title: t('routes.tenant.tenantList'),
        title: '楼栋管理',
        icon: 'ant-design:switcher-filled',
        policy: 'Buildings.BuildingsManagement.view',
      },
    },
  ],
};

export default tenant;
