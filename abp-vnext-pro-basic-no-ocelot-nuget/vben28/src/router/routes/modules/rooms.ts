import type { AppRouteModule } from '/@/router/types';
import { LAYOUT } from '/@/router/constant';
// import { t } from '/@/hooks/web/useI18n';
const tenant: AppRouteModule = {
  path: '/rooms',
  name: 'Rooms',
  component: LAYOUT,
  meta: {
    orderNo: 30,
    icon: 'ant-design:contacts-outlined',
    // title: t('教室申请'),
    title: '教室管理',
    policy: 'Rooms',
  },
  children: [
    {
      path: 'rooms',
      name: 'Rooms',
      component: () => import('/@/views/rooms/index.vue'),
      meta: {
        // title: t('routes.tenant.tenantList'),
        title: '教室管理',
        icon: 'ant-design:switcher-filled',
        policy: 'Rooms.RoomsManagement.view',
      },
    },
  ],
};

export default tenant;
