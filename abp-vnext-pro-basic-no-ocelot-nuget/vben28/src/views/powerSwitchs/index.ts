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

import {
  PowerSwitchsServiceProxy,
  PagePowerSwitchsInput,
  CreatePowerSwitchsInput,
  UpdatePowerSwitchsInput,
  DeletePowerSwitchsInput,
} from '/@/services/ServiceProxies';

export const searchFormSchema: FormSchema[] = [
  {
    field: 'roomId',
    label: t('routes.autoElectrictyControllerSys.RoomName'),
    component: 'Input',
    colProps: { span: 8 },
  },
  {
    field: 'serialNumber',
    label: t('routes.autoElectrictyControllerSys.SerialNumber'),
    component: 'Input',
    colProps: { span: 8 },
  },
  {
    field: 'controlledMachineName',
    label: t('routes.autoElectrictyControllerSys.ControlledMachineName'),
    component: 'Input',
    colProps: { span: 8 },
  },
  {
    field: 'isOnline',
    label: t('routes.autoElectrictyControllerSys.IsOnline'),
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
          label: '在线',
          value: 1,
          key: 1,
        },
        {
          label: '离线',
          value: 2,
          key: 2,
        },
      ],
    },
    labelWidth: 0,
    colProps: { span: 8 },
  },
  {
    field: 'status',
    label: t('routes.autoElectrictyControllerSys.Status'),
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
          label: '开闸',
          value: 1,
          key: 1,
        },
        {
          label: '合闸',
          value: 2,
          key: 2,
        },
      ],
    },
    labelWidth: 0,
    // show: false,
    colProps: { span: 8 },
  },
  {
    field: 'isAbnormal',
    label: t('routes.autoElectrictyControllerSys.IsAbnormal'),
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
          label: '异常',
          value: 1,
          key: 1,
        },
        {
          label: '正常',
          value: 2,
          key: 2,
        },
      ],
    },
    labelWidth: 0,
    // show: false,
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
    title: t('routes.autoElectrictyControllerSys.RoomName'),
    dataIndex: 'roomId',
    width: 100,
    resizable: true,
    align: 'left',
  },
  {
    title: t('routes.autoElectrictyControllerSys.SerialNumber'),
    dataIndex: 'serialNumber',
    resizable: true,
    width: 150,
  },
  {
    title: t('routes.autoElectrictyControllerSys.ControlledMachineName'),
    dataIndex: 'controlledMachineName',
    resizable: true,
    width: 350,
  },
  {
    title: t('routes.autoElectrictyControllerSys.IsOnline'),
    dataIndex: 'isOnline',
    resizable: true,
    width: 150,
  },
  {
    title: t('routes.autoElectrictyControllerSys.Status'),
    dataIndex: 'status',
    resizable: true,
    width: 150,
  },
  {
    title: t('routes.autoElectrictyControllerSys.IsAbnormal'),
    dataIndex: 'isAbnormal',
    resizable: true,
    width: 150,
  },
  {
    title: t('routes.autoElectrictyControllerSys.EnergyConsumption'),
    dataIndex: 'energyConsumption',
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

export const CreatePowerSwitchFormSchema: FormSchema[] = [
  {
    field: 'roomId',
    label: '所属教室',
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
    field: 'serialNumber',
    label: '设备序列号',
    component: 'Input',
    required: true,
    labelWidth: 150,
    colProps: { span: 20 },
  },
  {
    field: 'controlledMachineName',
    label: '控制器械名',
    component: 'Input',
    required: true,
    labelWidth: 150,
    // show: false,
    colProps: { span: 20 },
  },
  {
    field: 'isOnline',
    label: '是否在线',
    component: 'Select',
    defaultValue: 1,
    componentProps: {
      options: [
        {
          label: '离线',
          value: 2,
          key: 2,
        },
        {
          label: '在线',
          value: 1,
          key: 1,
        },
      ],
    },
    required: true,
    labelWidth: 150,
    colProps: { span: 20 },
  },
  {
    field: 'status',
    label: '开合闸状态',
    component: 'Select',
    defaultValue: 1,
    componentProps: {
      options: [
        {
          label: '合闸',
          value: 2,
          key: 2,
        },
        {
          label: '开闸',
          value: 1,
          key: 1,
        },
      ],
    },
    required: true,
    labelWidth: 150,
    // show: false,
    colProps: { span: 20 },
  },
];

export const UpdatePowerSwitchFormSchema: FormSchema[] = [
  {
    field: 'roomId',
    label: '所属教室',
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
    field: 'serialNumber',
    label: '设备序列号',
    component: 'Input',
    required: true,
    labelWidth: 150,
    colProps: { span: 20 },
  },
  {
    field: 'controlledMachineName',
    label: '控制器械名',
    component: 'Input',
    required: true,
    labelWidth: 150,
    // show: false,
    colProps: { span: 20 },
  },
  {
    field: 'isOnline',
    label: '是否在线',
    component: 'Select',
    defaultValue: 1,
    componentProps: {
      options: [
        {
          label: '离线',
          value: 2,
          key: 2,
        },
        {
          label: '在线',
          value: 1,
          key: 1,
        },
      ],
    },
    required: true,
    labelWidth: 150,
    colProps: { span: 20 },
  },
  {
    field: 'status',
    label: '开合闸状态',
    component: 'Select',
    defaultValue: 1,
    componentProps: {
      options: [
        {
          label: '合闸',
          value: 2,
          key: 2,
        },
        {
          label: '开闸',
          value: 1,
          key: 1,
        },
      ],
    },
    required: true,
    labelWidth: 150,
    // show: false,
    colProps: { span: 20 },
  },
];

/**
 * 分页查询空开
 * @param params
 * @returns
 */
export async function PagePowerSwitch(PagePowerSwitchsInput) {
  const powerSwitchsServiceProxy = new PowerSwitchsServiceProxy();
  return powerSwitchsServiceProxy.page(PagePowerSwitchsInput);
}

/**
 * 创建空开
 * @param params
 * @returns
 */
export async function CreatePowerSwitch({
  request,
  changeOkLoading,
  validate,
  closeModal,
  resetFields,
}) {
  changeOkLoading(true);
  await validate();
  const powerSwitchsServiceProxy = new PowerSwitchsServiceProxy();
  await powerSwitchsServiceProxy.create(request);
  changeOkLoading(false);
  message.success(t('common.operationSuccess'));
  resetFields();
  closeModal();
}

/**
 * 编辑空开
 * @param params
 * @returns
 */
export async function UpdatePowerSwitch({ request, changeOkLoading, validate, closeModal }) {
  console.log(request);
  changeOkLoading(true);
  await validate();
  const powerSwitchsServiceProxy = new PowerSwitchsServiceProxy();
  await powerSwitchsServiceProxy.update(request);
  changeOkLoading(false);
  message.success(t('common.operationSuccess'));
  closeModal();
}

/**
 * 删除空开
 * @param params
 * @returns
 */
export async function DeletePowerSwitch(powerSwitchId) {
  try {
    const powerSwitchsServiceProxy = new PowerSwitchsServiceProxy();
    openFullLoading();
    const request = new DeletePowerSwitchsInput();
    request.id = powerSwitchId;
    await powerSwitchsServiceProxy.delete(request);
    closeFullLoading();
    message.success(t('common.operationSuccess'));
  } catch (error) {
    closeFullLoading();
  }
}

/**
 * 无条件获取所有空开
 * @param params
 * @returns
 */
export async function GetAllPowerSwitchs() {
  const powerSwitchsServiceProxy = new PowerSwitchsServiceProxy();
  return await powerSwitchsServiceProxy.getAllPowerSwitchs();
}
