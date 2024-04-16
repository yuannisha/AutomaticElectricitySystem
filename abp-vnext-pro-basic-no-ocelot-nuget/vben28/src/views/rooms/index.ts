import { FormSchema } from '/@/components/Table';
import { BasicColumn } from '/@/components/Table';
import { useI18n } from '/@/hooks/web/useI18n';
import { formatToDateTime } from '/@/utils/dateUtil';
import { message } from 'ant-design-vue';
import { useLoading } from '/@/components/Loading';

const { t } = useI18n();
const [openFullLoading, closeFullLoading] = useLoading({
  tip: 'Loading...',
});

import { RoomsServiceProxy, PageRoomsInput, DeleteRoomsInput } from '/@/services/ServiceProxies';

export const searchFormSchema: FormSchema[] = [
  {
    field: 'building',
    label: t('routes.autoElectrictyControllerSys.Building'),
    component: 'Input',
    colProps: { span: 8 },
  },
  {
    field: 'floor',
    label: t('routes.autoElectrictyControllerSys.Floor'),
    component: 'Input',
    colProps: { span: 8 },
  },
  {
    field: 'nameOfRoom',
    label: t('routes.autoElectrictyControllerSys.NameOfRoom'),
    component: 'Input',
    colProps: { span: 8 },
  },
  {
    field: 'UsingOrNot',
    label: t('routes.autoElectrictyControllerSys.UsingOrNot'),
    component: 'Select',
    defaultValue: 0,
    componentProps: {
      options: [
        {
          label: '正在使用',
          value: 1,
          key: 1,
        },
        {
          label: '默认',
          value: 0,
          key: 0,
        },
        {
          label: '未使用',
          value: 2,
          key: 2,
        },
      ],
    },
    labelWidth: 0,
    colProps: { span: 8 },
  },
  {
    field: 'RoomType',
    label: t('routes.autoElectrictyControllerSys.RoomType'),
    component: 'Select',
    defaultValue: 0,
    componentProps: {
      options: [
        {
          label: '默认',
          value: 0,
          key: 0,
        },
        {
          label: '多媒体教室',
          value: 1,
          key: 1,
        },
        {
          label: '机房',
          value: 2,
          key: 2,
        },
        {
          label: '普通教室',
          value: 3,
          key: 3,
        },
      ],
    },
    labelWidth: 0,
    // show: false,
    colProps: { span: 8 },
  },
  {
    field: 'ControlType',
    label: t('routes.autoElectrictyControllerSys.ControlType'),
    component: 'Select',
    defaultValue: 0,
    componentProps: {
      options: [
        {
          label: '手动',
          value: 1,
          key: 1,
        },
        {
          label: '默认',
          value: 0,
          key: 0,
        },
        {
          label: '自动',
          value: 2,
          key: 2,
        },
      ],
    },
    labelWidth: 0,
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
    title: t('routes.autoElectrictyControllerSys.Building'),
    dataIndex: 'buildingId',
    width: 100,
    resizable: true,
    align: 'left',
  },
  {
    title: t('routes.autoElectrictyControllerSys.Floor'),
    dataIndex: 'floor',
    resizable: true,
    width: 150,
  },
  {
    title: t('routes.autoElectrictyControllerSys.NameOfRoom'),
    dataIndex: 'no',
    resizable: true,
    width: 350,
  },
  {
    title: t('routes.autoElectrictyControllerSys.RoomType'),
    dataIndex: 'roomType',
    resizable: true,
    width: 150,
  },
  {
    title: t('routes.autoElectrictyControllerSys.ControlType'),
    dataIndex: 'controlType',
    resizable: true,
    width: 150,
  },
  {
    title: t('routes.autoElectrictyControllerSys.IsUsingOrNot'),
    dataIndex: 'isUsingOrNot',
    resizable: true,
    width: 150,
  },
  {
    title: t('routes.autoElectrictyControllerSys.CreationTime'),
    dataIndex: 'creationTime',
    resizable: true,
    width: 150,
    customRender: ({ text }) => {
      return formatToDateTime(text);
    },
  },
];

export const CreateRoomFormSchema: FormSchema[] = [
  {
    field: 'BuildingId',
    label: '楼栋',
    component: 'Select',
    componentProps: {
      options: [],
    },
    required: true,
    labelWidth: 150,
    // show: false,
    colProps: { span: 20 },
  },
  {
    field: 'Floor',
    label: '楼层',
    component: 'Select',
    defaultValue: 1,
    componentProps: {
      options: [
        {
          label: '负一楼',
          value: -1,
          key: -1,
        },
        {
          label: '负二楼',
          value: -2,
          key: -2,
        },
        {
          label: '一楼',
          value: 1,
          key: 1,
        },
        {
          label: '二楼',
          value: 2,
          key: 2,
        },
        {
          label: '三楼',
          value: 3,
          key: 3,
        },
        {
          label: '四楼',
          value: 4,
          key: 4,
        },
        {
          label: '五楼',
          value: 5,
          key: 5,
        },
      ],
    },
    required: true,
    labelWidth: 150,
    colProps: { span: 20 },
  },
  {
    field: 'No',
    label: '教室名称',
    component: 'Input',
    required: true,
    labelWidth: 150,
    // show: false,
    colProps: { span: 20 },
  },
  {
    field: 'UsingOrNot',
    label: '是否在用',
    component: 'Select',
    defaultValue: 1,
    componentProps: {
      options: [
        {
          label: '正在使用',
          value: 1,
          key: 1,
        },
        {
          label: '未使用',
          value: 2,
          key: 2,
        },
      ],
    },
    required: true,
    labelWidth: 150,
    colProps: { span: 20 },
  },
  {
    field: 'RoomType',
    label: '教室类型',
    component: 'Select',
    defaultValue: 3,
    componentProps: {
      options: [
        {
          label: '普通教室',
          value: 3,
          key: 3,
        },
        {
          label: '多媒体教室',
          value: 1,
          key: 1,
        },
        {
          label: '机房',
          value: 2,
          key: 2,
        },
      ],
    },
    required: true,
    labelWidth: 150,
    // show: false,
    colProps: { span: 20 },
  },
  {
    field: 'ControlType',
    label: '控制类型',
    component: 'Select',
    defaultValue: 1,
    componentProps: {
      options: [
        {
          label: '手动',
          value: 1,
          key: 1,
        },
        {
          label: '自动',
          value: 2,
          key: 2,
        },
      ],
    },
    required: true,
    labelWidth: 150,
    colProps: { span: 20 },
  },
];

export const UpdateRoomFormSchema: FormSchema[] = [
  {
    field: 'BuildingId',
    label: '楼栋',
    component: 'Select',
    defaultValue: null,
    componentProps: {
      options: [],
    },
    required: true,
    labelWidth: 150,
    // show: false,
    colProps: { span: 20 },
  },
  {
    field: 'Floor',
    label: '楼层',
    component: 'Select',
    defaultValue: 1,
    componentProps: {
      options: [
        {
          label: '负一楼',
          value: -1,
          key: -1,
        },
        {
          label: '负二楼',
          value: -2,
          key: -2,
        },
        {
          label: '一楼',
          value: 1,
          key: 1,
        },
        {
          label: '二楼',
          value: 2,
          key: 2,
        },
        {
          label: '三楼',
          value: 3,
          key: 3,
        },
        {
          label: '四楼',
          value: 4,
          key: 4,
        },
        {
          label: '五楼',
          value: 5,
          key: 5,
        },
      ],
    },
    required: true,
    labelWidth: 150,
    colProps: { span: 20 },
  },
  {
    field: 'No',
    label: '教室名称',
    component: 'Input',
    defaultValue: '',
    required: true,
    labelWidth: 150,
    // show: false,
    colProps: { span: 20 },
  },
  {
    field: 'UsingOrNot',
    label: '是否在用',
    component: 'Select',
    defaultValue: 2,
    componentProps: {
      options: [
        {
          label: '正在使用',
          value: 1,
          key: 1,
        },
        {
          label: '未使用',
          value: 2,
          key: 2,
        },
      ],
    },
    required: true,
    labelWidth: 150,
    colProps: { span: 20 },
  },
  {
    field: 'RoomType',
    label: '教室类型',
    component: 'Select',
    defaultValue: 3,
    componentProps: {
      options: [
        {
          label: '普通教室',
          value: 3,
          key: 3,
        },
        {
          label: '多媒体教室',
          value: 1,
          key: 1,
        },
        {
          label: '机房',
          value: 2,
          key: 2,
        },
      ],
    },
    required: true,
    labelWidth: 150,
    // show: false,
    colProps: { span: 20 },
  },
  {
    field: 'ControlType',
    label: '控制类型',
    component: 'Select',
    defaultValue: 2,
    componentProps: {
      options: [
        {
          label: '手动',
          value: 1,
          key: 1,
        },
        {
          label: '自动',
          value: 2,
          key: 2,
        },
      ],
    },
    required: true,
    labelWidth: 150,
    colProps: { span: 20 },
  },
];

/**
 * 分页查询教室
 * @param params
 * @returns
 */
export async function PageRoom(PageRoomsInput) {
  const roomsServiceProxy = new RoomsServiceProxy();
  return roomsServiceProxy.page(PageRoomsInput);
}

/**
 * 创建教室
 * @param params
 * @returns
 */
export async function CreateRoom({ request, changeOkLoading, validate, closeModal, resetFields }) {
  changeOkLoading(true);
  await validate();
  const roomsServiceProxy = new RoomsServiceProxy();
  await roomsServiceProxy.create(request);
  changeOkLoading(false);
  message.success(t('common.operationSuccess'));
  resetFields();
  closeModal();
}

/**
 * 编辑教室
 * @param params
 * @returns
 */
export async function UpdateRoom({ request, changeOkLoading, validate, closeModal }) {
  console.log(request);
  changeOkLoading(true);
  await validate();
  const roomsServiceProxy = new RoomsServiceProxy();
  await roomsServiceProxy.update(request);
  changeOkLoading(false);
  message.success(t('common.operationSuccess'));
  closeModal();
}

/**
 * 删除教室
 * @param params
 * @returns
 */
export async function DeleteRoom(roomId) {
  try {
    const roomsServiceProxy = new RoomsServiceProxy();
    openFullLoading();
    const request = new DeleteRoomsInput();
    request.id = roomId;
    await roomsServiceProxy.delete(request);
    closeFullLoading();
    message.success(t('common.operationSuccess'));
  } catch (error) {
    closeFullLoading();
  }
}

/**
 * 无条件获取所有教室
 * @param params
 * @returns
 */
export async function GetAllRoomsUnConditional() {
  const roomsServiceProxy = new RoomsServiceProxy();
  return roomsServiceProxy.getAllClassrooms();
}
