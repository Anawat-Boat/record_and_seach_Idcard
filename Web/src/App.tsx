import "./App.css";
import TableCustomer from "./component/TableCustomer";

function App() {
  return (
    <>
      <section className=" flex flex-col items-center justify-center w-screen px-40 gap-10">
        <section className="text-black font-bold text-4xl">
          <div>Aioi Bangkok Insurance</div>
          <div>ไอโออิ กรุงเทพประกันภัย</div>
        </section>
        <TableCustomer />
      </section>
    </>
  );
}

export default App;
