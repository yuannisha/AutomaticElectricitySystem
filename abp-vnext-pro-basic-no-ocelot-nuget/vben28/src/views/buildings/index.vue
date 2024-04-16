<template>
  <div>
    <BasicTable @register="registerTable" size="small">
      <template #toolbar>
        <a-button
          preIcon="ant-design:plus-circle-outlined"
          type="primary"
          @click="openCreateBuildingModal"
          v-auth="'Buildings.BuildingsManagement.create'"
        >
          {{ t('common.createText') }}
        </a-button>
      </template>

      <template #action="{ record }">
        <TableAction
          :actions="[
            {
              icon: 'ant-design:edit-outlined',
              // auth: 'AbpIdentity.Users.Update',
              auth: 'Buildings.BuildingsManagement.edit',
              label: t('common.editText'),
              onClick: handleEdit.bind(null, record),
            },
          ]"
          :dropDownActions="[
            {
              // auth: 'AbpIdentity.Users.Delete',
              auth: 'Buildings.BuildingsManagement.delete',
              label: t('common.delText'),
              onClick: handleDelete.bind(null, record),
            },
          ]"
        />
      </template>
    </BasicTable>

    <div>
      <CreateBuilding
        @register="registerCreateBuildingModal"
        @reload="reload"
        :bodyStyle="{ 'padding-top': '0' }"
      />
    </div>
    <div>
      <EditBuilding
        @register="registerEditBuildingModal"
        @reload="reload"
        :bodyStyle="{ 'padding-top': '0' }"
      />
    </div>
  </div>
</template>

<script lang="ts">
  import { defineComponent } from 'vue';
  import { BasicTable, useTable, TableAction } from '/@/components/Table';
  import {
    tableColumns,
    searchFormSchema,
    PageBuilding,
    DeleteBuilding,
  } from '/@/views/buildings/index';
  import CreateBuilding from './CreateBuilding.vue';
  import { useMessage } from '/@/hooks/web/useMessage';
  import { useModal } from '/@/components/Modal';
  import { useI18n } from '/@/hooks/web/useI18n';
  import EditBuilding from './EditBuilding.vue';

  export default defineComponent({
    name: 'Buildings',
    components: {
      BasicTable,
      CreateBuilding,
      EditBuilding,
      TableAction,
    },
    setup() {
      const { t } = useI18n();
      const [registerCreateBuildingModal, { openModal: openCreateBuildingModal }] = useModal();
      const [registerEditBuildingModal, { openModal: openEditBuildingModal }] = useModal();

      // table配置
      const [registerTable, { reload }] = useTable({
        columns: tableColumns,
        formConfig: {
          labelWidth: 70,
          schemas: searchFormSchema,
          //   fieldMapToTime: [
          //     ["time", ["executionBeginTime", "executionEndTime"], "YYYY-MM-DD HH:mm:ss"]
          //   ]
        },
        api: PageBuilding,
        showTableSetting: true,
        useSearchForm: true,
        bordered: true,
        canResize: true,
        showIndexColumn: true,
        actionColumn: {
          width: 120,
          title: t('common.action'),
          dataIndex: 'action',
          slots: {
            customRender: 'action',
          },
          fixed: 'right',
        },
        immediate: true,
        scroll: { x: true },
      });
      const { createConfirm } = useMessage();

      // 编辑楼栋
      const handleEdit = (record: Recordable) => {
        openEditBuildingModal(true, {
          record: record,
        });
      };

      // 删除楼栋
      const handleDelete = async (record: Recordable) => {
        let msg = t('common.askDelete');
        createConfirm({
          iconType: 'warning',
          title: t('common.tip'),
          content: msg,
          onOk: async () => {
            await DeleteBuilding(record.id);
            await reload();
          },
        });
      };

      return {
        registerTable,
        reload,
        openCreateBuildingModal,
        registerCreateBuildingModal,
        t,
        handleEdit,
        registerEditBuildingModal,
        handleDelete,
      };
    },
  });
</script>
