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
  BuildingsServiceProxy,
  PageBuildingsInput,
  CreateBuildingsInput,
  UpdateBuildingsInput,
  DeleteBuildingsInput,
} from '/@/services/ServiceProxies';

export const searchFormSchema: FormSchema[] = [
  {
    field: 'name',
    label: t('routes.autoElectrictyControllerSys.BuildingName'),
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
    title: t('routes.autoElectrictyControllerSys.BuildingName'),
    dataIndex: 'name',
    width: 100,
    resizable: true,
    align: 'left',
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

export const CreateBuildingFormSchema: FormSchema[] = [
  {
    field: 'name',
    label: '楼栋名称',
    component: 'Input',
    required: true,
    labelWidth: 150,
    // show: false,
    colProps: { span: 20 },
  },
  {
    field: 'displayOrder',
    label: '展示序号',
    component: 'Input',
    required: true,
    labelWidth: 150,
    colProps: { span: 20 },
  },
];

export const UpdateBuildingFormSchema: FormSchema[] = [
  {
    field: 'name',
    label: '楼栋名称',
    component: 'Input',
    required: true,
    labelWidth: 150,
    // show: false,
    colProps: { span: 20 },
  },
  {
    field: 'displayOrder',
    label: '展示序号',
    component: 'Input',
    required: true,
    labelWidth: 150,
    colProps: { span: 20 },
  },
];

/**
 * 分页查询楼栋
 * @param params
 * @returns
 */
export async function PageBuilding(PageBuildingsInput) {
  const buildingsServiceProxy = new BuildingsServiceProxy();
  return buildingsServiceProxy.page(PageBuildingsInput);
}

/**
 * 创建楼栋
 * @param params
 * @returns
 */
export async function CreateBuilding({
  request,
  changeOkLoading,
  validate,
  closeModal,
  resetFields,
}) {
  changeOkLoading(true);
  await validate();
  const buildingsServiceProxy = new BuildingsServiceProxy();
  await buildingsServiceProxy.create(request);
  changeOkLoading(false);
  message.success(t('common.operationSuccess'));
  resetFields();
  closeModal();
}

/**
 * 编辑楼栋
 * @param param0
 */
export async function UpdateBuilding({ request, changeOkLoading, validate, closeModal }) {
  console.log(request);
  changeOkLoading(true);
  await validate();
  const buildingsServiceProxy = new BuildingsServiceProxy();
  await buildingsServiceProxy.update(request);
  changeOkLoading(false);
  message.success(t('common.operationSuccess'));
  closeModal();
}

/**
 * 删除楼栋
 * @param param0
 */
export async function DeleteBuilding(buildingId) {
  try {
    const buildingsServiceProxy = new BuildingsServiceProxy();
    openFullLoading();
    const request = new DeleteBuildingsInput();
    request.id = buildingId;
    await buildingsServiceProxy.delete(request);
    closeFullLoading();
    message.success(t('common.operationSuccess'));
  } catch (error) {
    closeFullLoading();
  }
}

/**
 * 无条件获取所有楼栋
 * @param params
 * @returns
 */
export async function GetAllBuildingsUnConditional() {
  const buildingsServiceProxy = new BuildingsServiceProxy();
  return buildingsServiceProxy.getAllBuilding();
}
