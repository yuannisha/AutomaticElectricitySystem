import { FormSchema } from '/@/components/Table';
import { BasicColumn } from '/@/components/Table';
import { useI18n } from '/@/hooks/web/useI18n';
import { formatToDateTime } from '/@/utils/dateUtil';

const { t } = useI18n();

import {
  AvailableTimespanInput,
  BookingInWeekendInput,
  BookingInformationsServiceProxy,
  CreateOrUpdateClassroomBookingInput,
  GetAllBookingInforInput,
  GuidListEntityDto,
  GuidNullableIdDto,
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
    field: 'studentClass',
    label: t('routes.autoElectrictyControllerSys.StudentClass'),
    component: 'Input',
    colProps: { span: 8 },
  },
  {
    field: 'usingClassroom',
    label: t('routes.autoElectrictyControllerSys.UsingClassroom'),
    component: 'Input',
    colProps: { span: 8 },
  },
  {
    field: 'bookingTimespan',
    label: t('routes.autoElectrictyControllerSys.BookingTimespan'),
    component: 'Input',
    colProps: { span: 8 },
  },
  {
    field: 'creationTime',
    label: t('routes.autoElectrictyControllerSys.CreationTime'),
    component: 'Input',
    colProps: { span: 8 },
  },
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
    title: t('routes.autoElectrictyControllerSys.StudentClass'),
    dataIndex: 'studentClass',
    resizable: true,
    width: 150,
  },
  {
    title: t('routes.autoElectrictyControllerSys.UsingClassroom'),
    dataIndex: 'usingClassroom',
    resizable: true,
    width: 150,
  },
  {
    title: t('routes.autoElectrictyControllerSys.BookingTimespan'),
    dataIndex: 'bookingTimespan',
    resizable: true,
    width: 550,
  },
  {
    title: t('routes.autoElectrictyControllerSys.UsingPurpose'),
    dataIndex: 'usingPurpose',
    resizable: true,
    width: 150,
  },
  {
    title: t('routes.autoElectrictyControllerSys.TelephoneNumber'),
    dataIndex: 'telephoneNumber',
    resizable: true,
    width: 180,
  },
  {
    title: t('routes.autoElectrictyControllerSys.CreationTime'),
    dataIndex: 'creationTime',
    resizable: true,
    width: 300,
    customRender: ({ text }) => {
      return formatToDateTime(text);
    },
  },
];

/**
 * 获取所有的申请信息
 * @param params
 * @returns
 */
export async function getAllBookingInfor(GetAllBookingInforInput) {
  const bookingInformationsServiceProxy = new BookingInformationsServiceProxy();
  return bookingInformationsServiceProxy.getAllBookingInfor(GetAllBookingInforInput);
}

/**
 * 插入申请信息
 * @param params
 * @returns
 */
export async function storeAndInsertNewInfor(params: CreateOrUpdateClassroomBookingInput) {
  const bookingInformationsServiceProxy = new BookingInformationsServiceProxy();
  return bookingInformationsServiceProxy.storeAndInsertNewInfor(
    CreateOrUpdateClassroomBookingInput,
  );
}

/**
 * 周末申请
 * @param params
 * @returns
 */
export async function bookingInWeekend(params: BookingInWeekendInput) {
  const bookingInformationsServiceProxy = new BookingInformationsServiceProxy();
  return bookingInformationsServiceProxy.bookingInWeekend(BookingInWeekendInput);
}

/**
 * 获得指定教室周末空闲时间段
 * @param params
 * @returns
 */
export async function getAvailableTimespanInforInWeekend(params: AvailableTimespanInput) {
  const bookingInformationsServiceProxy = new BookingInformationsServiceProxy();
  return bookingInformationsServiceProxy.getAvailableTimespanInforInWeekend(AvailableTimespanInput);
}

/**
 * 获取教室编辑
 * @param params
 * @returns
 */
export async function getClassroomBookingEdit(params: GuidNullableIdDto) {
  const bookingInformationsServiceProxy = new BookingInformationsServiceProxy();
  return bookingInformationsServiceProxy.getClassroomBookingEdit(GuidNullableIdDto);
}

/**
 * 创建申请信息
 * @param params
 * @returns
 */
export async function createClassroomBooking(params: CreateOrUpdateClassroomBookingInput) {
  const bookingInformationsServiceProxy = new BookingInformationsServiceProxy();
  return bookingInformationsServiceProxy.createClassroomBooking(
    CreateOrUpdateClassroomBookingInput,
  );
}

/**
 * 更新申请信息
 * @param params
 * @returns
 */
export async function updateClassroomBooking(params: CreateOrUpdateClassroomBookingInput) {
  const bookingInformationsServiceProxy = new BookingInformationsServiceProxy();
  return bookingInformationsServiceProxy.updateClassroomBooking(
    CreateOrUpdateClassroomBookingInput,
  );
}

/**
 * 删除申请信息
 * @param params
 * @returns
 */
export async function deleteClassroomBooking(params: GuidListEntityDto) {
  const bookingInformationsServiceProxy = new BookingInformationsServiceProxy();
  return bookingInformationsServiceProxy.deleteClassroomBooking(GuidListEntityDto);
}

/**
 * 获得指定教室空闲时间段
 * @param params
 * @returns
 */
export async function getAvailableTimespanInfor(params: AvailableTimespanInput) {
  const bookingInformationsServiceProxy = new BookingInformationsServiceProxy();
  return bookingInformationsServiceProxy.getAvailableTimespanInfor(AvailableTimespanInput);
}

/**
 * 身份信息确认
 * @param params
 * @returns
 */
export async function identityVerify(params: studentId, name, className) {
  const bookingInformationsServiceProxy = new BookingInformationsServiceProxy();
  return bookingInformationsServiceProxy.identityVerify(studentId, name, className);
}

/**
 * 无条件获取所有申请信息
 * @param params
 * @returns
 */
export async function getAllInformation() {
  const bookingInformationsServiceProxy = new BookingInformationsServiceProxy();
  return bookingInformationsServiceProxy.getAllInformation();
}
