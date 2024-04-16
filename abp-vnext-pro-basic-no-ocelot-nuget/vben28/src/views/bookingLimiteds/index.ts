import { FormSchema } from '/@/components/Table';
import { BasicColumn } from '/@/components/Table';
import { useI18n } from '/@/hooks/web/useI18n';
import { formatToDateTime } from '/@/utils/dateUtil';

const { t } = useI18n();

import {
  BookingLimitedsServiceProxy,
  PageBookingLimitedInput,
  PageBookingLimitedOutputPagedResultDto,
} from '/@/services/ServiceProxies';

export const searchFormSchema: FormSchema[] = [
  {
    field: 'studentId',
    label: t('routes.autoElectrictyControllerSys.StudentId'),
    component: 'Input',
    colProps: { span: 8 },
  },
  {
    field: 'studentName',
    label: t('routes.autoElectrictyControllerSys.StudentName'),
    component: 'Input',
    colProps: { span: 8 },
  },
  {
    field: 'date',
    label: t('routes.autoElectrictyControllerSys.Date'),
    component: 'Input',
    colProps: { span: 8 },
  },
  {
    field: 'bookedHours',
    label: t('routes.autoElectrictyControllerSys.BookedHours'),
    component: 'Input',
    colProps: { span: 8 },
  }
];

export const tableColumns: BasicColumn[] = [
  // {
  //   title: t('routes.admin.tenant'),
  //   dataIndex: 'tenantName',
  //   width: 100,
  // },
  {
    title: t('routes.autoElectrictyControllerSys.StudentId'),
    dataIndex: 'studentId',
    width: 100,
    resizable: true,
    align: 'left',
  },
  {
    title: t('routes.autoElectrictyControllerSys.StudentName'),
    dataIndex: 'studentName',
    resizable: true,
    width: 150,
  },
  {
    title: t('routes.autoElectrictyControllerSys.Date'),
    dataIndex: 'date',
    resizable: true,
    width: 350,
  },
  {
    title: t('routes.autoElectrictyControllerSys.BookedHours'),
    dataIndex: 'bookedHours',
    resizable: true,
    width: 150,
  },
];

/**
 * 获取所有的预约限制信息
 * @param params
 * @returns
 */
export async function PageBookingLimited(PageBookingLimitedInput) {
  const bookingLimitedsServiceProxy = new BookingLimitedsServiceProxy();
  return bookingLimitedsServiceProxy.page(PageBookingLimitedInput);
}
