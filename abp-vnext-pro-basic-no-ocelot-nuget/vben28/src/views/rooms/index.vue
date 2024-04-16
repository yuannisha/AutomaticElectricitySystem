<template>
  <div>
    <BasicTable @register="registerTable" size="small">
      <template #toolbar>
        <a-button
          preIcon="ant-design:plus-circle-outlined"
          type="primary"
          @click="handleCreateRoom"
          v-auth="'Rooms.RoomsManagement.create'"
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
              auth: 'Rooms.RoomsManagement.edit',
              label: t('common.editText'),
              onClick: handleEdit.bind(null, record),
            },
          ]"
          :dropDownActions="[
            {
              // auth: 'AbpIdentity.Users.Delete',
              auth: 'Rooms.RoomsManagement.delete',
              label: t('common.delText'),
              onClick: handleDelete.bind(null, record),
            },
          ]"
        />
      </template>
    </BasicTable>
    <div>
      <CreateRoom
        @register="registerCreateRoomModal"
        @reload="reload"
        :bodyStyle="{ 'padding-top': '0' }"
      />
    </div>
    <div>
      <EditRoom
        @register="registerEditRoomModal"
        @reload="reload"
        :bodyStyle="{ 'padding-top': '0' }"
      />
    </div>
  </div>
</template>

<script lang="ts">
  import { defineComponent } from 'vue';
  import { BasicTable, useTable, TableAction } from '/@/components/Table';
  import { tableColumns, searchFormSchema, PageRoom, DeleteRoom } from '/@/views/rooms/index';
  import { GetAllBuildingsUnConditional } from '/@/views/buildings/index';
  import EditRoom from './EditRoom.vue';
  import CreateRoom from './CreateRoom.vue';
  import { useI18n } from '/@/hooks/web/useI18n';
  import { useModal } from '/@/components/Modal';
  import { useMessage } from '/@/hooks/web/useMessage';
  export default defineComponent({
    name: 'Rooms',
    components: {
      BasicTable,
      CreateRoom,
      EditRoom,
      TableAction,
    },
    setup() {
      const { t } = useI18n();
      const [registerCreateRoomModal, { openModal: openCreateRoomModal }] = useModal();
      const [registerEditRoomModal, { openModal: openEditRoomModal }] = useModal();

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
        api: [PageRoom, GetAllBuildingsUnConditional],
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
      const handleCreateRoom = async () => {
        const buildingsInfor = await GetAllBuildingsUnConditional();
        openCreateRoomModal(true, {
          buildings: buildingsInfor,
        });
      };

      // 编辑教室
      const handleEdit = async (record: Recordable) => {
        const buildingsInformantion = await GetAllBuildingsUnConditional();
        openEditRoomModal(true, {
          record: record,
          buildings: buildingsInformantion,
        });
      };

      // 删除教室
      const handleDelete = async (record: Recordable) => {
        let msg = t('common.askDelete');
        createConfirm({
          iconType: 'warning',
          title: t('common.tip'),
          content: msg,
          onOk: async () => {
            await DeleteRoom(record.id);
            await reload();
          },
        });
      };

      return {
        handleDelete,
        handleEdit,
        registerTable,
        reload,
        t,
        registerCreateRoomModal,
        registerEditRoomModal,
        openCreateRoomModal,
        handleCreateRoom,
      };
    },
  });
</script>
