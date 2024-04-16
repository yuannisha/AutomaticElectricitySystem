import type { AppRouteModule } from '/@/router/types';
import { LAYOUT } from '/@/router/constant';
// import { t } from '/@/hooks/web/useI18n';
const tenant: AppRouteModule = {
  path: '/classroomBooking',
  name: 'ClassroomBooking',
  component: LAYOUT,
  meta: {
    orderNo: 30,
    icon: 'ant-design:contacts-outlined',
    // title: t('教室预约'),
    title: '教室预约管理',
    policy: 'BookingInformation',
  },
  children: [
    {
      path: 'classroomBooking',
      name: 'ClassroomBooking',
      component: () => import('/@/views/classroomBooking/index.vue'),
      meta: {
        // title: t('routes.tenant.tenantList'),
        title: '教室预约',
        icon: 'ant-design:switcher-filled',
        policy: 'BookingInformation.BookingInformationManagement.view',
      },
    },
  ],
};

export default tenant;
