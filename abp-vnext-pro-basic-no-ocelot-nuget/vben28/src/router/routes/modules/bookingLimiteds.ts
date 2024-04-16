import type { AppRouteModule } from '/@/router/types';
import { LAYOUT } from '/@/router/constant';
// import { t } from '/@/hooks/web/useI18n';
const tenant: AppRouteModule = {
  path: '/bookingLimiteds',
  name: 'BookingLimiteds',
  component: LAYOUT,
  meta: {
    orderNo: 40,
    icon: 'ant-design:contacts-outlined',
    // title: t('教室预约'),
    title: '教室预约限制信息',
    policy: 'BookingLimited',
  },
  children: [
    {
      path: 'bookingLimiteds',
      name: 'BookingLimiteds',
      component: () => import('/@/views/bookingLimiteds/index.vue'),
      meta: {
        // title: t('routes.tenant.tenantList'),
        title: '教室预约限制信息管理',
        icon: 'ant-design:switcher-filled',
        policy: 'BookingLimited.BookingLimitedManagement.view',
      },
    },
  ],
};

export default tenant;
