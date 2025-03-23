"use client";
import { useEffect, useState } from "react";
import { Table, Input, Button, Space } from "antd";
import api from "../utils/api";
import ModalCustomer from "./ModalCustomer";
import { DeleteTwoTone, EditTwoTone } from "@ant-design/icons";
interface DataType {
  id: number;
  fullName: string;
  cityzenId: string;
  birthDate: Date;
  address: string;
  createdDate: Date;
  updatedDate: Date;
}

const TableCustomer = () => {
  const [dataSource, setDataSource] = useState<DataType[]>([]);
  const [searchText, setSearchText] = useState("");
  const [loading, setLoading] = useState(false);
  const [isModalOpen, setIsModalOpen] = useState<boolean>(false);
  const [state, setState] = useState<"Create" | "Update">("Create");
  const [formId, setFormId] = useState<number>(0);

  useEffect(() => {
    if (!isModalOpen) {
      handleSearch("");
    }
  }, [isModalOpen]);

  const columns = [
    { title: "FullName", dataIndex: "fullName", key: "fullName" },
    { title: "CitizenId", dataIndex: "citizenId", key: "citizenId" },
    {
      title: "BirthDate",
      key: "birthDate",
      render: (_: any, data: DataType) => (
     
      <Space size="middle">
        <div>{data.birthDate.toLocaleString('YYYY-MM-DD')}</div>
      </Space>
    )
    },
    { title: "Address", dataIndex: "address", key: "address" },
    { title: "Created Date", dataIndex: "createdDate", key: "createdDate",
      render: (_: any, data: DataType) => (
     
      <Space size="middle">
        <div>{data.createdDate.toLocaleString('en-CA')}</div>
        {/* <div>{ dayjs(data.createdDate).format('YYYY-MM-DD')}</div> */}
      </Space>
    ) },
    { title: "Updated Date", dataIndex: "updatedDate", key: "updatedDate",
      render: (_: any, data: DataType) => (
     
      <Space size="middle">
        <div>{data.updatedDate.toLocaleString('en-CA')}</div>
        {/* <div>{ dayjs(data.updatedDate).format('YYYY-MM-DD')}</div> */}
      </Space>
    ) },
    {
      title: "Action",
      key: "action",
      render: (_: any, data: DataType) => (
        <Space size="large">
          <EditTwoTone
            onClick={async () => {
              handleUpdateCustomer(data.id);
            }}
          />
          <DeleteTwoTone
            onClick={async () => {
              await handleDeleteCustomer(data.id);
            }}
          />
        </Space>
      ),
    },
  ];

  const handleSearch = async () => {
    setLoading(true);
    await handleGetCustomer();
  };
  const handleGetCustomer = async () => {
    setLoading(true);
    await api
      .get(`/api/customer?search=${searchText}`)
      .then(({ data }) => {
        setDataSource(data.data);
      })
      .catch((error) => console.error("Error fetching data:", error))
      .finally(() => setLoading(false));
  };
  const handleNewCustomer = () => {
    setState("Create");
    setIsModalOpen(true);
  };
  const handleUpdateCustomer = (id: number) => {
    setFormId(id);
    setState("Update");
    setIsModalOpen(true);
  };
  const handleDeleteCustomer = async (id: number) => {
    setLoading(true);
    await api
      .delete(`/api/customer/${id}`)
      .then(({ data }) => {
        if (data.isSuccess) {
          handleGetCustomer();
        }
      })
      .finally(() => setLoading(false));
  };

  return (
    <section className="flex flex-col gap-3 w-full">
      <section className="flex flex-row gap-3 justify-between">
        <Input.Search
          placeholder="Search..."
          value={searchText}
          onSearch={handleSearch}
          onChange={(e) => setSearchText(e.target.value)}
          style={{ width: "40%" }}
          allowClear
          size="large"
        />
        <Button
          type="primary"
          onClick={handleNewCustomer}
          className="w-1/5"
          size="large"
        >
          New Customer
        </Button>
      </section>
      <Table
        rowKey={"id"}
        dataSource={dataSource}
        columns={columns}
        loading={loading}
        size="large"
        style={{ width: "100%", marginTop: "10px", height: "100%" }}
      />
      <ModalCustomer
        id={formId}
        state={state}
        isModalOpen={isModalOpen}
        setIsModalOpen={setIsModalOpen}
      />
    </section>
  );
};

export default TableCustomer;
function dayjs(birthDate: Date) {
  throw new Error("Function not implemented.");
}
