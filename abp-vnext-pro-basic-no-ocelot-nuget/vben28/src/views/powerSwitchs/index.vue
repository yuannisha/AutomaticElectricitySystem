<template>
  <div>
    <BasicTable @register="registerTable" size="small">
      <template #toolbar>
        <a-button
          preIcon="ant-design:plus-circle-outlined"
          type="primary"
          @click="handleCreatePowerSwitch"
          v-auth="'PowerSwitchs.PowerSwitchsManagement.create'"
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
              auth: 'PowerSwitchs.PowerSwitchsManagement.edit',
              label: t('common.editText'),
              onClick: handleEdit.bind(null, record),
            },
          ]"
          :dropDownActions="[
            {
              // auth: 'AbpIdentity.Users.Delete',
              auth: 'PowerSwitchs.PowerSwitchsManagement.delete',
              label: t('common.delText'),
              onClick: handleDelete.bind(null, record),
            },
          ]"
        />
      </template>
    </BasicTable>
    <div>
      <CreatePowerSwitch
        @register="registerCreatePowerSwitchModal"
        @reload="reload"
        :bodyStyle="{ 'padding-top': '0' }"
      />
    </div>
    <div>
      <EditPowerSwitch
        @register="registerEditPowerSwitchModal"
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
    PagePowerSwitch,
    DeletePowerSwitch,
  } from '/@/views/powerSwitchs/index';
  import { GetAllRoomsUnConditional } from '/@/views/rooms/index';
  import { useI18n } from '/@/hooks/web/useI18n';
  import { useModal } from '/@/components/Modal';
  import { useMessage } from '/@/hooks/web/useMessage';
  import EditPowerSwitch from './EditPowerSwitch.vue';
  import CreatePowerSwitch from './CreatePowerSwitch.vue';

  export default defineComponent({
    name: 'PowerSwitchs',
    components: {
      BasicTable,
      CreatePowerSwitch,
      EditPowerSwitch,
      TableAction,
    },
    setup() {
      const { t } = useI18n();
      const [registerCreatePowerSwitchModal, { openModal: openCreatePowerSwitchModal }] =
        useModal();
      const [registerEditPowerSwitchModal, { openModal: openEditPowerSwitchModal }] = useModal();

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
        api: [PagePowerSwitch, GetAllRoomsUnConditional],
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

      //创建教室
      const handleCreatePowerSwitch = async () => {
        const roomsInfor = await GetAllRoomsUnConditional();
        openCreatePowerSwitchModal(true, {
          rooms: roomsInfor,
        });
      };

      // 编辑空开
      const handleEdit = async (record: Recordable) => {
        const roomsInfor = await GetAllRoomsUnConditional();
        openEditPowerSwitchModal(true, {
          record: record,
          rooms: roomsInfor,
        });
      };

      // 删除空开
      const handleDelete = async (record: Recordable) => {
        let msg = t('common.askDelete');
        createConfirm({
          iconType: 'warning',
          title: t('common.tip'),
          content: msg,
          onOk: async () => {
            await DeletePowerSwitch(record.id);
            await reload();
          },
        });
      };

      return {
        registerTable,
        reload,
        registerEditPowerSwitchModal,
        registerCreatePowerSwitchModal,
        openCreatePowerSwitchModal,
        t,
        handleDelete,
        handleEdit,
        handleCreatePowerSwitch,
      };
    },
  });
</script>
