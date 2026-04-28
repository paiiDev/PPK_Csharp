function renderBlogs() {
  const list = getBlogs();
console.log(list);
const $tableBody = $("#tblBlogs").html("");
$tableBody.empty();

  list.forEach((blog, index) => {
    const row = `
         <tr class="hover:bg-gray-50">
       
        <td class="px-4 py-2 text-gray-700">${index + 1}</td>
        <td class="px-4 py-2 text-green-600 font-semibold">
        ${blog.title}
        </td>
         <td class="px-4 py-2 text-green-600 font-semibold">
        ${blog.content}
        </td>
         <td class="px-4 py-2 text-green-600 font-semibold">
        ${blog.author}
        </td>
         <td  class=" px-4 py-2 text-gray-700">
            <button  class="btn-edit bg-blue-500 text-white px-4 py-2 rounded hover:bg-blue-600 transition" 
            data-id="${blog.id}">Edit</button>
            <button class="btn-delete bg-red-500 text-white px-4 py-2 rounded hover:bg-red-600 transition" 
            data-id="${blog.id}">Delete</button>
        </td>
      </tr>
        `;
    $("#tblBlogs").append(row);
  });
}
