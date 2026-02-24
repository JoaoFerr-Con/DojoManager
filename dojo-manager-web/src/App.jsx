import { useEffect, useState } from 'react'
import axios from 'axios'
import { BrowserRouter as Router, Routes, Route, Link } from 'react-router-dom'
import { 
  BarChart, Bar, XAxis, YAxis, Tooltip, 
  ResponsiveContainer, CartesianGrid, Cell 
} from 'recharts'
import { 
  Users, DollarSign, LayoutDashboard, UserMinus, ChevronRight, 
  TrendingUp, Award, PlusCircle, Search, X, Shield, Pencil, CheckCircle, LogOut,
  Trash2
} from 'lucide-react'

// IMPORT CONFIRMADO POR VOCÊ
import Login from "./components/Login";

// --- FUNÇÕES DE AUXÍLIO ---
const maskPhone = (v) => {
  if (!v) return ""; v = v.replace(/\D/g, "");
  v = v.replace(/^(\d{2})(\d)/g, "($1) $2"); v = v.replace(/(\d{5})(\d)/, "$1-$2");
  return v.substring(0, 15);
}

// REGRA DE GRADUAÇÃO
const FAIXAS = ['Branca', 'Azul', 'Roxa', 'Marrom', 'Preta'];
const getProximaGraduacao = (faixa, graus) => {
  let novosGraus = (graus || 0) + 1;
  let novaFaixa = faixa;
  if (novosGraus > 4) {
    novosGraus = 0;
    const idx = FAIXAS.indexOf(faixa);
    if (idx < FAIXAS.length - 1) novaFaixa = FAIXAS[idx + 1];
  }
  return { novaFaixa, novosGraus };
}

const getBeltColorClass = (f) => {
  const c = { 'Branca': 'bg-white border-2 border-slate-200', 'Azul': 'bg-blue-600', 'Roxa': 'bg-purple-600', 'Marrom': 'bg-amber-900', 'Preta': 'bg-slate-950' }
  return c[f] || 'bg-slate-400'
}

const getModalityInfo = (m) => {
  const i = { 0: { label: 'Jiu-Jitsu', icon: '🥋' }, 1: { label: 'Boxe', icon: '🥊' }, 2: { label: 'Muay Thai', icon: '🥊📿' } }
  return i[m] || { label: 'Outro', icon: '👊' }
}

// --- COMPONENTES ATÔMICOS ---
const SidebarLink = ({ to, icon, label }) => (
  <Link to={to} className="flex items-center justify-between p-3 rounded-xl text-white/70 hover:bg-white/10 hover:text-white transition-all group">
    <div className="flex items-center gap-3">
      <span className="text-white/40 group-hover:text-red-500 transition-colors">{icon}</span>
      <span className="font-semibold text-sm">{label}</span>
    </div>
    <ChevronRight size={14} className="opacity-0 group-hover:opacity-100" />
  </Link>
)

const StatCard = ({ label, value, color, icon: Icon }) => (
  <div className="bg-white p-6 rounded-2xl border border-slate-100 shadow-sm flex items-center gap-4">
    <div className={`p-3 rounded-xl ${color.replace('text', 'bg')}/10 ${color}`}><Icon size={24} /></div>
    <div><p className="text-[10px] font-black text-slate-400 uppercase">{label}</p><p className="text-2xl font-bold">{value}</p></div>
  </div>
)

const DashboardHome = ({ alunos, mensalidades }) => {
  const receita = mensalidades.filter(m => m.status === 1).reduce((acc, curr) => acc + curr.valor, 0)
  const dados = mensalidades.filter(m => m.status === 1).reduce((acc, m) => {
    const mes = new Date(m.dataVencimento).toLocaleDateString('pt-BR', { month: 'short' }).toUpperCase()
    const ex = acc.find(i => i.name === mes); ex ? ex.total += m.valor : acc.push({ name: mes, total: m.valor });
    return acc;
  }, []).slice(-6);

  return (
    <div className="space-y-8 animate-in fade-in duration-500">
      <div className="grid grid-cols-1 md:grid-cols-3 gap-6">
        <StatCard label="Atletas Ativos" value={alunos.filter(a => a.estaAtivo).length} color="text-green-600" icon={Users} />
        <StatCard label="Inativos" value={alunos.filter(a => !a.estaAtivo).length} color="text-slate-400" icon={UserMinus} />
        <StatCard label="Arrecadação" value={`R$ ${receita.toLocaleString('pt-BR', { minimumFractionDigits: 2 })}`} color="text-blue-600" icon={TrendingUp} />
      </div>
      <div className="grid grid-cols-1 lg:grid-cols-2 gap-6">
        <div className="bg-white p-6 rounded-3xl border border-slate-100 h-64 shadow-sm">
          <h3 className="text-[10px] font-black text-slate-400 uppercase tracking-widest mb-4">Faturamento Mensal (R$)</h3>
          <ResponsiveContainer width="100%" height="80%">
            <BarChart data={dados}><CartesianGrid strokeDasharray="3 3" vertical={false} stroke="#f1f5f9" /><XAxis dataKey="name" axisLine={false} tickLine={false} tick={{fontSize: 10, fontWeight: 'bold'}} /><Tooltip /><Bar dataKey="total" radius={[6, 6, 0, 0]} barSize={32}>{dados.map((e, i) => <Cell key={i} fill={i === dados.length - 1 ? '#ef4444' : '#0f172a'} />)}</Bar></BarChart>
          </ResponsiveContainer>
        </div>
        {/* BANNER REINTEGRADO */}
        <div className="bg-slate-950 rounded-3xl p-8 text-white relative overflow-hidden flex flex-col justify-center shadow-xl">
          <div className="relative z-10"><h2 className="text-2xl font-bold mb-2 italic tracking-tighter">Performance Financeira</h2><p className="text-slate-400 text-sm max-w-xs">A gestão automatizada permite focar no treino no tatame em Belém.</p></div>
          <Award className="absolute -right-4 -bottom-4 text-white/5 w-40 h-40" />
        </div>
      </div>
    </div>
  )
}

const AlunosList = ({ alunos, ativos, onToggleStatus, onEdit, onDelete, onGraduar }) => {
  const [busca, setBusca] = useState('')
  const filtrados = alunos.filter(a => a.estaAtivo === ativos && a.nome.toLowerCase().includes(busca.toLowerCase()))

  return (
    <div className="space-y-6 animate-in fade-in duration-300">
      <div className="flex flex-col md:flex-row justify-between items-center gap-4">
        <h2 className="text-xl font-bold text-slate-800">{ativos ? "Ativos no Tatame" : "Inativos"} <span className="ml-2 text-sm font-normal text-slate-400 bg-slate-100 px-2 py-0.5 rounded-full">{filtrados.length}</span></h2>
        <div className="relative w-full md:w-72"><Search className="absolute left-3 top-2.5 text-slate-400" size={16} /><input type="text" placeholder="Buscar atleta..." className="w-full pl-10 pr-4 py-2 bg-slate-50 border rounded-xl outline-none focus:ring-2 focus:ring-red-500" onChange={e => setBusca(e.target.value)} /></div>
      </div>
      <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4">
        {filtrados.map(aluno => (
          <div key={aluno.id} className="p-5 rounded-2xl border border-slate-100 bg-white shadow-sm hover:shadow-md transition-all group">
            <div className="flex justify-between items-start mb-4">
              <div className="flex items-center gap-3">
                <div className={`w-1.5 h-10 rounded-full ${getBeltColorClass(aluno.faixaAtual)}`}></div>
                <div><h3 className="font-bold text-slate-900 leading-tight">{aluno.nome}</h3><p className="text-[10px] uppercase font-black text-slate-400 mt-0.5">{getModalityInfo(aluno.modalidade).icon} {getModalityInfo(aluno.modalidade).label} • {aluno.faixaAtual}</p></div>
              </div>
              <div className="flex gap-1 opacity-0 group-hover:opacity-100 transition-opacity">
                <button onClick={() => onEdit(aluno)} className="p-2 text-slate-400 hover:text-blue-600 rounded-lg"><Pencil size={18}/></button>
                <button onClick={() => onToggleStatus(aluno.id)} className="p-2 text-slate-400 hover:text-orange-600 rounded-lg">{ativos ? <UserMinus size={18}/> : <TrendingUp size={18}/>}</button>
                <button onClick={() => onDelete(aluno.id)} className="p-2 text-slate-400 hover:text-red-600 rounded-lg"><Trash2 size={18}/></button>
              </div>
            </div>
            <div className="bg-slate-50 p-3 rounded-xl flex justify-between items-center mt-2">
              <div className="flex gap-1">{[1,2,3,4].map(i => <div key={i} className={`w-2 h-2 rounded-full ${aluno.graus >= i ? 'bg-red-500 shadow-sm' : 'bg-slate-200'}`}></div>)}</div>
              {aluno.modalidade === 0 && ativos && (
                <button onClick={() => onGraduar(aluno)} className="text-[10px] font-black text-blue-600 uppercase flex items-center gap-1 hover:text-blue-800 transition-colors"><PlusCircle size={14}/> Evoluir</button>
              )}
            </div>
          </div>
        ))}
      </div>
    </div>
  )
}

const FinanceiroList = ({ mensalidades, onPagar }) => {
  const [fMes, setFMes] = useState(new Date().getMonth());
  const meses = ["Janeiro", "Fevereiro", "Março", "Abril", "Maio", "Junho", "Julho", "Agosto", "Setembro", "Outubro", "Novembro", "Dezembro"];
  const filtrados = mensalidades.filter(m => new Date(m.dataVencimento).getMonth() === fMes);
  return (
    <div className="space-y-6">
      <div className="flex gap-4 items-center bg-slate-50 p-4 rounded-2xl border w-fit"><label className="text-xs font-bold text-slate-400 uppercase">Período:</label>
      <select value={fMes} onChange={(e) => setFMes(parseInt(e.target.value))} className="bg-white border p-2 rounded-xl text-sm font-bold outline-none">{meses.map((m, i) => <option key={i} value={i}>{m}</option>)}</select></div>
      <div className="bg-white border rounded-3xl shadow-sm overflow-hidden text-sm"><table className="w-full text-left"><thead className="bg-slate-50 border-b"><tr><th className="p-4 uppercase text-slate-400">Atleta</th><th className="p-4 uppercase text-slate-400">Vencimento</th><th className="p-4 uppercase text-slate-400">Valor</th><th className="p-4 text-center uppercase text-slate-400">Status</th><th className="p-4"></th></tr></thead>
      <tbody className="divide-y">{filtrados.map(m => (<tr key={m.id} className="hover:bg-slate-50/50"><td className="p-4 font-bold">{m.nomeAluno || 'Atleta'}</td><td className="p-4 text-slate-500">{new Date(m.dataVencimento).toLocaleDateString()}</td><td className="p-4 font-bold">R$ {m.valor.toFixed(2)}</td><td className="p-4 text-center"><span className={`px-3 py-1 rounded-full text-[10px] font-black uppercase ${m.status === 1 ? 'bg-green-100 text-green-700' : 'bg-orange-100 text-orange-700'}`}>{m.status === 1 ? 'Pago' : 'Pendente'}</span></td><td className="p-4 text-right">{m.status === 0 && <button onClick={() => onPagar(m.id)} className="text-green-600 hover:bg-green-50 p-2 rounded-lg"><CheckCircle size={20}/></button>}</td></tr>))}</tbody></table></div>
    </div>
  );
}

export default function App() {
  const [token, setToken] = useState(localStorage.getItem('token'))
  const [alunos, setAlunos] = useState([]); const [mensalidades, setMensalidades] = useState([])
  const [modals, setModals] = useState({ add: false, edit: false })
  const [forms, setForms] = useState({ add: { nome: '', telefone: '', dataNascimento: '', sexo: 0, modalidade: 0 }, edit: null })
  const API_URL = 'http://localhost:5192/api'; const config = { headers: { Authorization: `Bearer ${token}` } }

  const handleLogout = () => { localStorage.removeItem('token'); setToken(null); }

  const fetchData = async () => {
    if (!token) return
    try {
      const [resA, resM] = await Promise.all([axios.get(`${API_URL}/Alunos`, config), axios.get(`${API_URL}/Mensalidades`, config)])
      setAlunos(resA.data); setMensalidades(resM.data)
    } catch (e) { if (e.response?.status === 401) handleLogout() }
  }

  const handleMatricula = async (e) => {
    e.preventDefault(); try { await axios.post(`${API_URL}/Alunos`, forms.add, config); setModals({ ...modals, add: false }); fetchData() } catch (e) { alert("Erro ao matricular.") }
  }

  const handleUpdate = async (e) => {
    e.preventDefault(); try { await axios.put(`${API_URL}/Alunos/${forms.edit.id}`, forms.edit, config); setModals({ ...modals, edit: false }); fetchData() } catch (e) { alert("Erro ao editar.") }
  }

  const handleGraduar = async (aluno) => {
    const { novaFaixa, novosGraus } = getProximaGraduacao(aluno.faixaAtual, aluno.graus);
    try { await axios.put(`${API_URL}/Alunos/${aluno.id}`, { ...aluno, faixaAtual: novaFaixa, graus: novosGraus }, config); fetchData() } catch (e) { alert("Erro ao graduar.") }
  }

  const handleToggle = async (id) => { await axios.put(`${API_URL}/Alunos/${id}/status`, {}, config); fetchData() }
  const handleDelete = async (id) => { if (window.confirm("Excluir?")) { await axios.delete(`${API_URL}/Alunos/${id}`, config); fetchData() } }
  const handlePagar = async (id) => { await axios.put(`${API_URL}/Mensalidades/pagar/${id}`, {}, config); fetchData() }

  useEffect(() => { fetchData() }, [token])
  if (!token) return <Login onLogin={setToken} />

  return (
    <Router>
      <div className="flex min-h-screen bg-slate-50 font-sans">
        <aside className="w-72 bg-slate-950 h-screen sticky top-0 p-6 flex flex-col border-r border-slate-900 shadow-2xl z-20 text-white">
          <div className="mb-12 pl-2"><h1 className="text-3xl font-black italic tracking-tighter">DOJO<span className="text-red-600">MANAGER</span></h1><p className="text-[10px] uppercase tracking-widest text-slate-500 font-bold mt-1">Unidade Belém / PA</p></div>
          <nav className="flex-1 space-y-2">
            <SidebarLink to="/" icon={<LayoutDashboard size={20} />} label="Visão Geral" /><SidebarLink to="/alunos" icon={<Users size={20} />} label="Ativos" /><SidebarLink to="/inativos" icon={<UserMinus size={20} />} label="Inativos" /><SidebarLink to="/financeiro" icon={<DollarSign size={20} />} label="Financeiro" />
          </nav>
          <button onClick={handleLogout} className="mb-4 flex items-center gap-3 p-3 rounded-xl text-red-400 hover:bg-red-500/10 transition-all font-bold text-xs uppercase"><LogOut size={18} /> Sair do Sistema</button>
          <button onClick={() => setModals({ ...modals, add: true })} className="bg-red-600 text-white font-bold p-4 rounded-2xl hover:bg-red-700 shadow-lg flex items-center justify-center gap-2"><PlusCircle size={20} /> NOVA MATRÍCULA</button>
        </aside>
        <main className="flex-1 p-10 max-w-7xl mx-auto"><div className="bg-white p-8 rounded-3xl shadow-xl border min-h-[600px]"><Routes>
          <Route path="/" element={<DashboardHome alunos={alunos} mensalidades={mensalidades} />} />
          <Route path="/alunos" element={<AlunosList alunos={alunos} ativos={true} onToggleStatus={handleToggle} onDelete={handleDelete} onGraduar={handleGraduar} onEdit={(a) => { setForms({ ...forms, edit: { ...a, dataNascimento: a.dataNascimento.split('T')[0] } }); setModals({ ...modals, edit: true }) }} />} />
          <Route path="/inativos" element={<AlunosList alunos={alunos} ativos={false} onToggleStatus={handleToggle} onDelete={handleDelete} onEdit={(a) => { setForms({ ...forms, edit: { ...a, dataNascimento: a.dataNascimento.split('T')[0] } }); setModals({ ...modals, edit: true }) }} />} />
          <Route path="/financeiro" element={<FinanceiroList mensalidades={mensalidades} onPagar={handlePagar} />} />
        </Routes></div></main>

        {modals.add && (
          <div className="fixed inset-0 bg-slate-900/60 backdrop-blur-sm z-50 flex items-center justify-center p-4"><div className="bg-white w-full max-w-lg rounded-3xl shadow-2xl animate-in zoom-in"><div className="bg-slate-950 p-6 text-white flex justify-between items-center rounded-t-3xl"><h3 className="text-xl font-bold italic">Nova Matrícula</h3><button onClick={() => setModals({ ...modals, add: false })}><X/></button></div>
          <form onSubmit={handleMatricula} className="p-8 grid grid-cols-2 gap-5"><div className="col-span-2"><label className="text-[10px] font-black uppercase text-slate-400 ml-1">Nome</label><input type="text" required className="w-full p-3 bg-slate-50 border rounded-xl outline-none" value={forms.add.nome} onChange={e => setForms({...forms, add: {...forms.add, nome: e.target.value}})}/></div>
          <div><label className="text-[10px] font-black uppercase text-slate-400 ml-1">WhatsApp</label><input type="tel" required placeholder="(91) 90000-0000" className="w-full p-3 bg-slate-50 border rounded-xl outline-none" value={forms.add.telefone} onChange={e => setForms({...forms, add: {...forms.add, telefone: maskPhone(e.target.value)}})}/></div>
          <div><label className="text-[10px] font-black uppercase text-slate-400 ml-1">Nascimento</label><input type="date" required className="w-full p-3 bg-slate-50 border rounded-xl" value={forms.add.dataNascimento} onChange={e => setForms({...forms, add: {...forms.add, dataNascimento: e.target.value}})}/></div>
          <div><label className="text-[10px] font-black uppercase text-slate-400 ml-1">Sexo</label><select className="w-full p-3 bg-slate-50 border rounded-xl" value={forms.add.sexo} onChange={e => setForms({...forms, add: {...forms.add, sexo: parseInt(e.target.value)}})}><option value={0}>M</option><option value={1}>F</option></select></div>
          <div><label className="text-[10px] font-black uppercase text-slate-400 ml-1">Modalidade</label><select className="w-full p-3 bg-slate-50 border rounded-xl font-bold text-red-600" value={forms.add.modalidade} onChange={e => setForms({...forms, add: {...forms.add, modalidade: parseInt(e.target.value)}})}><option value={0}>Jiu-Jitsu 🥋</option><option value={1}>Boxe 🥊</option><option value={2}>Muay Thai 🥊📿</option></select></div>
          <button type="submit" className="col-span-2 bg-red-600 text-white font-bold py-4 rounded-2xl hover:bg-red-700 mt-4 shadow-xl">FINALIZAR</button></form></div></div>
        )}

        {modals.edit && forms.edit && (
          <div className="fixed inset-0 bg-slate-900/60 backdrop-blur-sm z-50 flex items-center justify-center p-4"><div className="bg-white w-full max-w-lg rounded-3xl shadow-2xl animate-in zoom-in"><div className="bg-slate-950 p-6 text-white flex justify-between items-center rounded-t-3xl"><h3 className="text-xl font-bold italic">Editar Atleta</h3><button onClick={() => setModals({ ...modals, edit: false })}><X/></button></div>
          <form onSubmit={handleUpdate} className="p-8 grid grid-cols-2 gap-5"><div className="col-span-2"><label className="text-[10px] font-black uppercase text-slate-400 ml-1">Nome</label><input type="text" required className="w-full p-3 bg-slate-50 border rounded-xl outline-none" value={forms.edit.nome} onChange={e => setForms({...forms, edit: {...forms.edit, nome: e.target.value}})} /></div>
          <div><label className="text-[10px] font-black uppercase text-slate-400 ml-1">WhatsApp</label><input type="tel" required className="w-full p-3 bg-slate-50 border rounded-xl outline-none" value={forms.edit.telefone} onChange={e => setForms({...forms, edit: {...forms.edit, telefone: maskPhone(e.target.value)}})} /></div>
          <div><label className="text-[10px] font-black uppercase text-slate-400 ml-1">Nascimento</label><input type="date" required className="w-full p-3 bg-slate-50 border rounded-xl" value={forms.edit.dataNascimento} onChange={e => setForms({...forms, edit: {...forms.edit, dataNascimento: e.target.value}})} /></div>
          <button type="submit" className="col-span-2 bg-blue-600 text-white font-bold py-4 rounded-2xl hover:bg-blue-700 mt-4 shadow-xl">SALVAR ALTERAÇÕES</button></form></div></div>
        )}
      </div>
    </Router>
  )
}